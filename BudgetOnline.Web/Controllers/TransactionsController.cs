using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Extensions;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.Editors;
using BudgetOnline.UI.Models.SelectItems;
using BudgetOnline.UI.Models.ViewCommands;
using BudgetOnline.Web.Infrastructure.Binders;
using BudgetOnline.Web.Infrastructure.Core;
using BudgetOnline.Web.Infrastructure.Extensions;
using BudgetOnline.Web.Infrastructure.Helpers;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Controllers
{
    public class TransactionsController : ListWithSearchController<TransactionListSearchViewModel>
    {
        public IAccountRepository AccountRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public ITransactionRepository TransactionRepository { get; set; }
        public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
        public ITransactionLinkRepository TransactionLinkRepository { get; set; }
        public ITransactionTagRepository TransactionTagRepository { get; set; }
        public ICurrencyRepository CurrencyRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }
        public ITransactionDataHelper TransactionDataHelper { get; set; }
        public ISettingsHelper SettingsHelper { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
        public ITransactionCalculator TransactionCalculator { get; set; }
        public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }
        public ITotalsOnDateUpdater TotalsOnDateUpdater { get; set; }
        public ITransactionCacheHelper TransactionCacheHelper { get; set; }

        protected override string SearchCacheKey { get { return "transactions_search_cache_key"; } }
        private readonly int[] _pageSizes = new[] { 10, 25, 50, 100 };



        [HttpGet]
        [RestrictRole(Roles.FactView)]
        public ActionResult List()
        {
            TransactionListSearchViewModel search = !IsSearchLastUsed ? GetDefaultSearchModel() : SearchLastUsed;
            if (!string.IsNullOrWhiteSpace(Request.Params["page"]))
            {
                var page = int.Parse(Request.Params["page"]);
                if (page > 0)
                    search.CurrentPage = page;
            }
            if (!string.IsNullOrWhiteSpace(Request.Params["pageSize"]))
            {
                var pageSize = int.Parse(Request.Params["pageSize"]);
                if (pageSize > 0)
                {
                    search.PageSize = pageSize;
                    SettingsHelper.SetWebSetting(CurrentUser.SectionId, CurrentUser.Id, "PageSize", pageSize);
                }
            }

            SearchLastUsed = search;

            var model = new TransactionListViewModel
                            {
                                Search = search,
                            };

            model = GetModel(SearchLastUsed);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.FactView)]
        public ActionResult List(FormCollection form)
        {
            SearchLastUsed = ParseSearchOptions(form);

            if (Request.IsAjaxRequest())
            {
                var model = GetModel(SearchLastUsed);
                return View("~/Views/Transactions/_List.cshtml", model);
            }

            return List();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model =
                new TransactionEditViewModel
                {
                    Amount = 1,
                };

            PopulateEditViewModelVariablesFromRequest(model);

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.FactAdd)]
        public ActionResult Create([ModelBinder(typeof(CustomViewModelBinder))] TransactionEditViewModel model)
        {
            var errors = model.Errors();
            if (ModelState.IsValid && !errors.Any())
            {
                var links = ConvertModelToLinkedTransactions(model);

                links = SaveLinkedTransactions(links);

                SaveTransactionTags(links.First.Id, links.First.Tags);

                RefreshBalanceStatistics();

                if (model.IsCreateNewAfterSave)
                    return RedirectToAction(
                        "create",
                        new
                        {
                            infoMessage = "record-was-saved",
                            savedid = links.First.Id,
                            date = model.Date.ToShortDateString(),
                            category = model.Category.Id,
                            account = model.TransactionType.Id == (int)TransactionTypes.Income ? model.SumIn.Account.Id : model.SumOut.Account.Id
                        });

                return Redirect(Url.Action("list") + string.Format("#t{0}", links.First.Id));
            }

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var linked = TransactionRepository.GetLinked(id);
            if (linked == null || linked.First == null)
                return new HttpNotFoundResult();

            if (!MembershipHelper.UsersInOneSection(MembershipHelper.CurrentUser.Id, linked.First.CreatedBy))
                return new HttpNotFoundResult();
            if (linked.Second != null && !MembershipHelper.UsersInOneSection(MembershipHelper.CurrentUser.Id, linked.Second.CreatedBy))
                return new HttpNotFoundResult();


            var model = ConvertLinkedTransactionToModel(linked);

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.FactAdd)]
        public ActionResult Edit([ModelBinder(typeof(CustomViewModelBinder))] TransactionEditViewModel model)
        {
            var errors = model.Errors();
            if (ModelState.IsValid && !errors.Any())
            {
                var previousLinks = TransactionRepository.GetLinked(model.Id);
                var previousDate = previousLinks.First.Date;

                var previousTags = TransactionTagRepository.GetByTransaction(model.Id);

                var links = ConvertModelToLinkedTransactions(model);
                links.First.Id = previousLinks.First.Id;
                if (links.Second != null)
                    links.Second.Id = previousLinks.Second != null ? previousLinks.Second.Id : 0;

                SaveLinkedTransactions(links);

                SaveTransactionTags(links.First.Id, links.First.Tags, previousTags);

                RefreshBalanceStatistics();

                if (links.First.Date == DateTimeProvider.Now().Date)
                    TotalsOnDateUpdater.UpdateData(links.First.Date);
                if (links.First.Date != previousDate)
                    TotalsOnDateUpdater.UpdateData(previousDate);

                return Redirect(Url.Action("list") + string.Format("#t{0}", links.First.Id));
            }

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.FactAdd)]
        public ActionResult SeparateTransaction(int id, string Sum)
        {
            var linked = TransactionRepository.GetLinked(id);
            if (linked == null || linked.First == null)
                return new HttpNotFoundResult();

            if (!MembershipHelper.UsersInOneSection(MembershipHelper.CurrentUser.Id, linked.First.CreatedBy))
                return new HttpNotFoundResult();
            if (linked.Second != null && !MembershipHelper.UsersInOneSection(MembershipHelper.CurrentUser.Id, linked.Second.CreatedBy))
                return new HttpNotFoundResult();

            var newId = id;

            var errors = new StringBuilder();
            if (linked.First.TransactionTypeId != (int)TransactionTypes.Outcome)
            {
                errors.AppendLine("Операция возможна только для расходов!");
                LogWriter.WarnFormat("Available obly for outcome. Id={0}, CurrentType={1}", id, (TransactionTypes)linked.First.TransactionTypeId);
            }

            //var sum = Request.Form["Sum"];
            decimal sumParsed = 0;
            if (string.IsNullOrWhiteSpace(Sum) || decimal.TryParse(Sum, out sumParsed))
            {
                errors.AppendLine("Ошибка обработки запроса!");
                LogWriter.WarnFormat("Form data passed with errors. Id={0}, Form.Keys={1}", id, Request.Form.AllKeys);
            }

            if (sumParsed >= linked.First.Sum)
            {
                errors.AppendLine(string.Format("Новая сумма должны быть меньше исходной ({0})!", Math.Abs(linked.First.Sum).ToString(CultureInfo.CurrentUICulture)));
                LogWriter.InfoFormat("Sum grater then original. Id={0}, OldSum={1}, NewSum={2}", id, linked.First.Sum, sumParsed);
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = new
                           {
                               ErrorMessage = errors.ToString(),
                               Success = errors.Length == 0,
                               RedirectUrl = Url.Action("Edit", new { id = newId })
                           }
            };
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var transaction = TransactionRepository.GetById(id);
            if (transaction == null)
                return new HttpNotFoundResult();

            if (!MembershipHelper.UsersInOneSection(MembershipHelper.GetUser().Id, transaction.CreatedBy))
                return new HttpNotFoundResult();

            TransactionRepository.Delete(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult GetTags(string term)
        {
            var tags = TransactionTagRepository.GetByNamePart(CurrentUser.SectionId, term).ToArray();
            var transactionCombined = TransactionRepository.GetTagsByNamePart(CurrentUser.SectionId, term).ToArray();


            return new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = tags.Concat(transactionCombined)
                    };
        }

        #region Overrides

        public TransactionListViewModel GetModel(TransactionListSearchViewModel search)
        {
            var searchOptions = new TransactionSearchOptions
                                {
                                    PageSize = search.PageSize,
                                    PageNumber = search.CurrentPage,
                                    Date1 = search.FromDate,
                                    Date2 = search.ToDate,
                                    SearchText = search.Text,
                                };

            var totalsSearchOptions = AutoMapper.Mapper.DynamicMap<TransactionSearchOptions, TransactionStatisticsSearchOptions>(searchOptions);

            var transactions = TransactionRepository.GetList(CurrentUser.SectionId, searchOptions);
            var transactionsTotal = TransactionStatisticsRepository.GetTotalsByCurrencies(CurrentUser.SectionId, totalsSearchOptions).ToList();

            var defaultCurrency = Dictionaries.Currencies().FirstOrDefault(o => o.IsDefault);

            var totalsByDates = TransactionCalculator.TotalsByDates(transactions.Where(o => o.TransactionTypeId == (int)TransactionTypes.Outcome), defaultCurrency.Id).ToList();

            var model = new TransactionListViewModel
                            {
                                Search = search,

                                Groups = ConvertTransactionsToListViewModel(transactions).ToList()
                                    .GroupBy(o => o.Date.Date)
                                    .Select(dt => new TransactionListGroupedViewModel
                                                    {
                                                        Title = string.Format("{0}      {1} {2}",
                                                            dt.Key.Year != DateTimeProvider.Now().Year
                                                                ? dt.Key.ToString("dd MMMM yyyy, dddd", CultureInfo.CurrentUICulture)
                                                                : dt.Key.ToString("dd MMMM, dddd", CultureInfo.CurrentUICulture),
                                                            totalsByDates.Where(x => x.Date == dt.Key.Date).Select(x => x.Sum.ToHtmlString(HtmlOutputFormatterExtensions.OutputLengthType.Normal)).FirstOrDefault(),
                                                            defaultCurrency.Name),
                                                        Items = dt.Select(o => o).Where(t => t.Date.Date == dt.Key)
                                                    }).ToList(),

                                Totals = transactionsTotal
                                  .GroupBy(o => new { o.CurrencyId, o.CurrencyName, o.CurrencySymbol })
                                  .Select(dt => new TransactionStatisticViewModel
                                                  {
                                                      CurrencyId = dt.Key.CurrencyId.Value,
                                                      CurrencyName = dt.Key.CurrencyName,
                                                      CurrencySymbol = dt.Key.CurrencySymbol,
                                                      Balance = dt.Sum(o => o.Sum)
                                                  })
                            };


            var totalRowsCount = Convert.ToDecimal(transactionsTotal.Sum(o => o.Count));
            search.PagesCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRowsCount / search.PageSize.Value)));

            return model;
        }

        #endregion

        #region Helpers

        private TransactionListSearchViewModel ParseSearchOptions(FormCollection form)
        {
            var result = GetDefaultSearchModel();
            result.Text = form["search"];

            if (!string.IsNullOrWhiteSpace(form["fromDate"]))
            {
                DateTime dt;
                if (DateTime.TryParse(form["fromDate"], CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out dt))
                    result.FromDate = dt.Date;
            }
            if (!SearchLastUsed.FromDate.HasValue)
                result.FromDate = DateTimeProvider.Now().Date.AddMonths(-2);

            if (!string.IsNullOrWhiteSpace(form["toDate"]))
            {
                DateTime dt;
                if (DateTime.TryParse(form["toDate"], CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out dt))
                    result.ToDate = dt.Date;
            }
            if (!string.IsNullOrWhiteSpace(form["page"]))
            {
                int n;
                if (int.TryParse(form["page"], NumberStyles.Integer, CultureInfo.CurrentUICulture, out n))
                    result.CurrentPage = n;
            }
            //if (!string.IsNullOrWhiteSpace(form["pageSize"]))
            //{
            //    int n;
            //    if (int.TryParse(form["pageSize"], NumberStyles.Integer, CultureInfo.CurrentUICulture, out n))
            //    {
            //        result.PageSize = n;
            //        var old = SettingsHelper.GetWebSetting(CurrentUser.SectionId, CurrentUser.Id, "PageSize", 0);
            //        if (old == 0 || old != n)
            //            SettingsHelper.SetWebSetting(CurrentUser.SectionId, CurrentUser.Id, "PageSize", n);
            //    }
            //}
            if (!result.PageSize.HasValue || result.PageSize.Value == 0)
                result.PageSize = SettingsHelper.GetWebSetting(CurrentUser.SectionId, CurrentUser.Id, "PageSize", 30);

            return result;
        }

        private TransactionListSearchViewModel GetDefaultSearchModel()
        {
            var pageSize = SettingsHelper.GetWebSetting(CurrentUser.SectionId, CurrentUser.Id, "PageSize", 0);
            return new TransactionListSearchViewModel
                    {
                        FromDate = DateTimeProvider.Now().Date.AddMonths(-2),
                        Pages = new SelectItemsModel
                        {
                            Items = _pageSizes.Select(o => new SelectItemModel
                            {
                                Text = o.ToString(CultureInfo.CurrentUICulture),
                                Value = o.ToString(CultureInfo.CurrentUICulture),
                                Selected = o.Equals(pageSize)
                            })
                        },
                        PageSize = pageSize,
                        CurrentPage = 1
                    };
        }

        private TransactionEditViewModel ConvertLinkedTransactionToModel(LinkedTransactions linked)
        {
            var sumIn = new CurrencyBundle();
            var sumOut = new CurrencyBundle();

            CheckForRecovery(linked);

            if (linked.IsLinked)
            {
                sumOut = new CurrencyBundle
                {
                    Sum = Math.Abs(linked.First.Sum),
                    Formula = linked.First.Formula,
                    Currency = new IdWithSelectList { Id = linked.First.CurrencyId },
                    Account = new IdWithSelectList { Id = linked.First.AccountId },
                };

                sumIn = new CurrencyBundle
                {
                    Sum = Math.Abs(linked.Second.Sum),
                    Formula = linked.Second.Formula,
                    Currency = new IdWithSelectList { Id = linked.Second.CurrencyId },
                    Account = new IdWithSelectList { Id = linked.Second.AccountId },
                };
            }
            else
            {
                if (linked.First.TransactionTypeId == (int)TransactionTypes.Income)
                    sumIn = new CurrencyBundle
                    {
                        Sum = Math.Abs(linked.First.Sum),
                        Formula = linked.First.Formula,
                        Currency = new IdWithSelectList { Id = linked.First.CurrencyId },
                        Account = new IdWithSelectList { Id = linked.First.AccountId },
                    };
                else if (linked.First.TransactionTypeId == (int)TransactionTypes.Outcome)
                    sumOut = new CurrencyBundle
                    {
                        Sum = Math.Abs(linked.First.Sum),
                        Formula = linked.First.Formula,
                        Currency = new IdWithSelectList { Id = linked.First.CurrencyId },
                        Account = new IdWithSelectList { Id = linked.First.AccountId },
                    };
                else if (linked.First.TransactionTypeId == (int)TransactionTypes.Transfer)
                {
                    sumIn = new CurrencyBundle
                    {
                        Sum = Math.Abs(linked.First.Sum),
                        Formula = linked.First.Formula,
                        Currency = new IdWithSelectList { Id = linked.First.CurrencyId },
                        Account = new IdWithSelectList { Id = linked.First.AccountId },
                    };
                    sumOut = new CurrencyBundle
                    {
                        Sum = Math.Abs(linked.First.Sum),
                        Formula = linked.First.Formula,
                        Currency = new IdWithSelectList { Id = linked.First.CurrencyId },
                        Account = new IdWithSelectList { Id = linked.First.AccountId },
                    };
                }
            }

            var model =
                new TransactionEditViewModel
                {
                    SumIn = sumIn,
                    SumOut = sumOut,
                    Category = new IdWithSelectList { Id = linked.First.CategoryId ?? 0 },
                    TransactionType = new IdWithSelectList { Id = linked.First.TransactionTypeId },
                    Amount = linked.First.Amount,
                    Id = linked.First.Id,
                    IsDisabled = linked.First.IsDisabled,
                    LinkedId = linked.Second != null ? (int?)linked.Second.Id : null,
                    Tags = linked.First.Tags,
                    Description = linked.First.Description,
                    CreatedBy = linked.First.CreatedBy,
                    CreatedWhen = linked.First.CreatedWhen,
                    Date = linked.First.Date,
                    UpdatedBy = linked.First.UpdatedBy,
                    UpdatedWhen = linked.First.UpdatedWhen
                };

            return model;
        }

        private void CheckForRecovery(LinkedTransactions linked)
        {
            if (linked.First.TransactionTypeId == (int)TransactionTypes.Transfer && linked.Second == null)
            {
                LogWriter.WarnFormat("Transaction recovery logic implemented for TransactionId={0}", linked.First.Id);

                linked.Second = Fasterflect.CloneExtensions.DeepClone(linked.First);
                linked.Second.Id = 0;
                linked.Second.Sum = TransactionDataHelper.GetRealSumValue(linked.First.TransactionTypeId, linked.First.Sum);
            }
        }

        private LinkedTransactions ConvertModelToLinkedTransactions(TransactionEditViewModel model)
        {
            var linked = new LinkedTransactions();
            var type = (TransactionTypes)model.TransactionType.Id;

            linked.First = ConvertModelToTransacion(model);

            if (type == TransactionTypes.Income)
            {
                linked.First.CurrencyId = model.SumIn.Currency.Id;
                linked.First.AccountId = model.SumIn.Account.Id;
                linked.First.Formula = model.SumIn.Formula;
                linked.First.Sum = TransactionDataHelper.GetRealSumValue(type, model.SumIn.Sum);
            }
            else
            {
                linked.First.CurrencyId = model.SumOut.Currency.Id;
                linked.First.AccountId = model.SumOut.Account.Id;
                linked.First.Formula = model.SumOut.Formula;
                linked.First.Sum = TransactionDataHelper.GetRealSumValue(TransactionTypes.Outcome, model.SumOut.Sum);
            }

            if (type == TransactionTypes.Exchange)
            {
                linked.Second = ConvertModelToTransacion(model);

                linked.Second.AccountId = model.SumOut.Account.Id;

                linked.Second.CurrencyId = model.SumIn.Currency.Id;
                linked.Second.Formula = model.SumIn.Formula;
                linked.Second.Sum = TransactionDataHelper.GetRealSumValue(TransactionTypes.Income, model.SumIn.Sum);
            }

            if (type == TransactionTypes.Transfer)
            {
                linked.Second = ConvertModelToTransacion(model);

                linked.Second.AccountId = model.SumIn.Account.Id;

                linked.Second.CurrencyId = model.SumOut.Currency.Id;
                linked.Second.Formula = model.SumOut.Formula;
                linked.Second.Sum = TransactionDataHelper.GetRealSumValue(TransactionTypes.Income, model.SumOut.Sum);
            }

            return linked;
        }

        private Transaction ConvertModelToTransacion(TransactionEditViewModel model)
        {
            var t = new Transaction
                    {
                        Id = model.Id,
                        SectionId = CurrentUser.SectionId,
                        Amount = model.Amount,
                        TransactionTypeId = model.TransactionType.Id,
                        CategoryId = model.Category.Id,
                        Date = model.Date,
                        Description = model.Description,
                        IsDisabled = model.IsDisabled,
                        Tags = TransactionDataHelper.NormalizeTags(model.Tags),
                    };

            if (model.Id == 0)
            {
                t.CreatedBy = CurrentUser.Id;
            }
            else
            {
                t.UpdatedBy = CurrentUser.Id;
            }

            return t;
        }

        private IEnumerable<TransactionListItemViewModel> ConvertTransactionsToListViewModel(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var model =
                    new TransactionListItemViewModel
                    {
                        Id = transaction.Id,
                        Date = transaction.Date,
                        Amount = transaction.Amount,
                        Tags = transaction.Tags,
                        IsDisabled = transaction.IsDisabled,
                        Description = transaction.Description,
                        CreatedWhen = transaction.CreatedWhen,
                        UpdatedWhen = transaction.UpdatedWhen,

                        CurrencySource = transaction.CurrencyNameSource,
                        CurrencyTarget = transaction.CurrencyNameTarget,

                        AccountTarget = transaction.AccountNameTarget,
                        AccountSource = transaction.AccountNameSource,

                        SumSource = transaction.SumSource,
                        SumTarget = transaction.SumTarget,
                    };

                PopulateCommands(model);

                yield return model;
            }
        }

        private void PopulateCommands(TransactionListItemViewModel transaction)
        {
            var commands = new List<ViewCommandUIModel>
			               	{
			               		new ViewCommandUIModel
			               			{
			               				IsDefault = true,
			               				Text = "Редактировать",
			               				IconCssClass = "cus-page-edit",
			               				Command = new RedirectViewCommandModel {Path = Url.Action("edit", new { id = transaction.Id})}
			               			},
			               		new ViewCommandUIModel
			               			{
                                        IsDividerBefore = true,
			               				IsDefault = false,
			               				Text = "Удалить",
			               				IconCssClass = "cus-page-edit",
			               				Command = new JsViewCommandModel{ClientFunction = "deleteRecord", Data = transaction.Id.ToString(CultureInfo.CurrentUICulture)}
			               			}
			               	};

            transaction.Commands = commands;
        }



        private void SaveTransactionTags(int transactionId, string tags, IEnumerable<TransactionTag> oldTags = null)
        {
            var parsedItems = TransactionDataHelper.ParseStringWithTags(tags);

            var tagItems = parsedItems.Where(t => oldTags == null || !oldTags.Any(oldTag => oldTag.Tag == t));
            foreach (var tagItem in tagItems)
                if (!string.IsNullOrWhiteSpace(tagItem))
                {
                    var tag = new TransactionTag
                                {
                                    Id = 0,
                                    Tag = tagItem,
                                    TransactionId = transactionId,
                                    SectionId = CurrentUser.SectionId,
                                    IsDisabled = false,
                                    CreatedBy = CurrentUser.Id,
                                    CreatedWhen = DateTimeProvider.Now(DateTimeKind.Utc)
                                };

                    TransactionTagRepository.Insert(tag);
                }



            if (oldTags != null)
                foreach (var tag in oldTags.Where(o => !parsedItems.Contains(o.Tag)).Select(oldTag => new TransactionTag
                                                                {
                                                                    Id = oldTag.Id,
                                                                }))
                {
                    TransactionTagRepository.Delete(tag);
                }
        }

        private SelectItemsModel GetAccountsList(int defaultId = 0)
        {
            var items = Dictionaries.Accounts()
                .Where(o => !o.IsDisabled)
                .Select(o => new SelectItemModel { Selected = o.Id.Equals(defaultId) || (defaultId == 0 && o.IsDefault), Text = o.Name, Value = o.Id.ToString() })
                .ToList();

            return new SelectItemsModel(items);
        }

        private SelectItemsModel GetCategoriesList(int defaultId = 0)
        {
            var items = Dictionaries.Categories()
                .Where(o => !o.IsDisabled)
                .Select(o => new SelectItemModel { Selected = o.Id.Equals(defaultId) || (defaultId == 0 && o.IsDefault), Text = o.Name, Value = o.Id.ToString() })
                .ToList();

            return new SelectItemsModel(items);
        }

        private SelectItemsModel GetTransactionTypes(int defaultId = 0)
        {
            var items =
                from TransactionTypes item in Enum.GetValues(typeof(TransactionTypes))
                select
                    new SelectItemModel
                    {
                        Text = item.GetDisplayAttr().Name,
                        Value = ((int)item).ToString(CultureInfo.InvariantCulture),
                        Icon = GetTransactiontypeIconClass(item)
                        //Selected = defaultId == (int)item
                    };

            return DictionaryHelper.GetDictionary(() => items,
                o => o.Value,
                o => o.Text,
                o => o.Icon,
                o => true,
                defaultId.ToString(CultureInfo.CurrentUICulture));
        }

        private string GetTransactiontypeIconClass(TransactionTypes type)
        {
            switch (type)
            {
                case TransactionTypes.Income:
                    return "cus-calculator-add";
                case TransactionTypes.Outcome:
                    return "cus-calculator-delete";
                case TransactionTypes.Transfer:
                    return "cus-table-relationship";
                case TransactionTypes.Exchange:
                    return "cus-table-refresh";
                default:
                    return "";
            }
        }

        private SelectItemsModel GetCurrenciesList(int defaultId = 0)
        {
            var items = Dictionaries.Currencies()
                .Where(o => !o.IsDisabled)
                .Select(o => new SelectItemModel { Selected = o.Id.Equals(defaultId) || (defaultId == 0 && o.IsDefault), Text = o.Name, Value = o.Id.ToString() })
                .ToList();

            return new SelectItemsModel(items);
        }

        private int GetIdForSelectList(IdWithSelectList list)
        {
            if (list.Id != 0)
                return list.Id;

            var defaultItem = list.Items.Items.FirstOrDefault(o => o.Selected);
            if (defaultItem != null)
                return int.Parse(defaultItem.Value);

            return 0;
        }

        private void PopulateListVariablesInEditViewModel(TransactionEditViewModel model)
        {
            model.Category = new IdWithSelectList { Id = model.Category.Id, Items = GetCategoriesList(model.Category.Id) };
            model.Category.Id = GetIdForSelectList(model.Category);

            model.TransactionType = new IdWithSelectList { Id = model.TransactionType.Id, Items = GetTransactionTypes(model.TransactionType.Id) };

            model.SumIn.Account = new IdWithSelectList { Id = model.SumIn.Account.Id, Items = GetAccountsList(model.SumIn.Account.Id) };
            model.SumIn.Account.Id = GetIdForSelectList(model.SumIn.Account);
            model.SumIn.Currency = new IdWithSelectList { Id = model.SumIn.Currency.Id, Items = GetCurrenciesList(model.SumIn.Currency.Id) };
            model.SumIn.Currency.Id = GetIdForSelectList(model.SumIn.Currency);

            model.SumOut.Account = new IdWithSelectList { Id = model.SumOut.Account.Id, Items = GetAccountsList(model.SumOut.Account.Id) };
            model.SumOut.Account.Id = GetIdForSelectList(model.SumOut.Account);
            model.SumOut.Currency = new IdWithSelectList { Id = model.SumOut.Currency.Id, Items = GetCurrenciesList(model.SumOut.Currency.Id) };
            model.SumOut.Currency.Id = GetIdForSelectList(model.SumOut.Currency);
        }

        private void PopulateEditViewModelVariablesFromRequest(TransactionEditViewModel model)
        {
            var qr = HttpContext.Request.QueryString;

            if (!string.IsNullOrWhiteSpace(qr["date"]))
            {
                DateTime date;
                if (DateTime.TryParse(qr["date"], CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out date))
                    model.Date = date.Date;
            }

            if (!string.IsNullOrWhiteSpace(qr["category"]))
            {
                int categoryId;
                if (int.TryParse(qr["category"], out categoryId))
                {
                    model.Category = new IdWithSelectList { Id = categoryId, Items = GetCategoriesList(categoryId) };
                }
            }

            if (!string.IsNullOrWhiteSpace(qr["account"]))
            {
                int accountId;
                if (int.TryParse(qr["account"], out accountId))
                {
                    model.SumIn.Account = new IdWithSelectList { Id = accountId, Items = GetAccountsList(accountId) };
                    model.SumOut.Account = new IdWithSelectList { Id = accountId, Items = GetAccountsList(accountId) };
                }
            }

            if (!string.IsNullOrWhiteSpace(qr["savedid"]))
            {
                model.IsCreateNewAfterSave = true;
            }
        }

        private Transaction InsertOrUpdate(Transaction transaction)
        {
            if (transaction.Id == 0)
                return TransactionRepository.Insert(transaction);

            TransactionRepository.Update(transaction);
            return transaction;
        }

        private LinkedTransactions SaveLinkedTransactions(LinkedTransactions links)
        {
            links.First = InsertOrUpdate(links.First);
            //links.First = TransactionRepository.Insert(links.First);
            if (links.Second != null)
            {
                bool isSecondNew = links.Second.Id == 0;

                links.Second.LinkedId = links.First.Id;
                links.Second = InsertOrUpdate(links.Second);

                if (isSecondNew)
                    TransactionLinkRepository.Insert(new TransactionLink
                    {
                        ChildId = links.Second.Id,
                        ParentId = links.First.Id,
                        CreatedWhen = DateTimeProvider.Now(DateTimeKind.Utc),
                        CreatedBy = MembershipHelper.CurrentUser.Id,
                    });
            }
            else
            {
                var link = TransactionLinkRepository.GetByTransactionId(links.First.Id);
                if (link != null)
                {
                    TransactionLinkRepository.Delete(link.Id);
                    TransactionRepository.Delete(link.ChildId != links.First.Id ? link.ChildId : link.ParentId);
                }
            }
            return links;
        }

        private void RefreshBalanceStatistics()
        {
            TransactionCacheHelper.BalanceUpdated();
        }

        #endregion
    }
}
