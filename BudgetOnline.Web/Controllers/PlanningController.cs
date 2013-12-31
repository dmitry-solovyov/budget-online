using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
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
    public class PlanningController : ListWithSearchController<PlannedTransactionsSearchViewModel>
    {
        public IPlannedTransactionRepository PlannedTransactionRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }
        public ITransactionDataHelper TransactionDataHelper { get; set; }
        public IPlannedTransactionCalculator PlannedTransactionCalculator { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }

        protected override string SearchCacheKey { get { return "planning_search_cache_key"; } }

        [HttpGet]
        [RestrictRole(Roles.PlanView)]
        public ActionResult List(string applyDate)
        {
            if (!string.IsNullOrWhiteSpace(applyDate))
            {
                DateTime applyDateParsed;
                if (DateTime.TryParse(applyDate, out applyDateParsed))
                    return View(GetModel(new PlannedTransactionsSearchViewModel(), applyDateParsed));
            }

            return View(GetModel(new PlannedTransactionsSearchViewModel()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PlannedTransactionEditViewModel();

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.PlanAdd)]
        public ActionResult Create([ModelBinder(typeof(CustomViewModelBinder))] PlannedTransactionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = PlannedTransactionRepository.Insert(
                    ConvertModelToPlannedTransaction(model));

                if (model.IsCreateNewAfterSave)
                    return RedirectToAction(
                        "create",
                        new
                        {
                            infoMessage = "record-was-saved",
                            savedid = transaction.Id,
                            account = model.Sum.Account.Id
                        });

                return RedirectToAction("list", new { infoMessage = "saved" });
            }

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var transaction = PlannedTransactionRepository.GetById(id);
            if (transaction == null)
                return new HttpNotFoundResult();

            if (!MembershipHelper.UsersInOneSection(MembershipHelper.GetUser().Id, transaction.CreatedBy))
                return new HttpNotFoundResult();

            PlannedTransactionRepository.Delete(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var transaction = PlannedTransactionRepository.GetById(id);
            if (transaction == null)
                return new HttpNotFoundResult();

            if (!MembershipHelper.UsersInOneSection(MembershipHelper.GetUser().Id, transaction.CreatedBy))
                return new HttpNotFoundResult();


            var model = ConvertPlannedTransactionToModel(transaction);

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.PlanAdd)]
        public ActionResult Edit([ModelBinder(typeof(CustomViewModelBinder))] PlannedTransactionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var previous = PlannedTransactionRepository.GetById(model.Id);

                PlannedTransactionRepository.Update(
                    ConvertModelToPlannedTransaction(model));


                return RedirectToAction(
                    "list",
                    new
                    {
                        InfoMessage = "saved",
                        savedid = model.Id,
                    });
            }

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult BuildTotals(DateTime? fromDate, DateTime? toDate)
        {
            var model = new BuildTotalsViewModel
                        {
                            FinalDate = DateTimeProvider.Now().Date.AddYears(1),
                            TargetCurrency = new IdWithSelectList { Id = 0, Items = GetCurrenciesList() }
                        };

            return View(model);
        }

        [HttpPost]
        public ActionResult BuildTotals([ModelBinder(typeof(CustomViewModelBinder))] BuildTotalsViewModel model)
        {
            var options = new PlannedTransactionSearchOptions
                            {
                                Date1 = DateTimeProvider.Now().FirstDateOfMonth(),
                                Date2 = model.FinalDate,
                            };

            try
            {
                IEnumerable<TransactionTotal> generated =
                    PlannedTransactionCalculator.GenerateActual(CurrentUser.SectionId, options, model.TargetCurrency.Id,
                                                                model.UseActualTotals).ToList();

                var dates = generated.Select(o => o.Date.Value).Distinct().OrderBy(o => o).ToList();

                var currency =
                    GetCurrenciesList(model.TargetCurrency.Id).Items.FirstOrDefault(
                        o => o.Value.Equals(model.TargetCurrency.Id.ToString()));

                model.Items = dates
                    .Select(o => new GeneratedPlannedTransactionListGroupedViewModel
                                     {
                                         Date =
                                             o.Month == DateTimeProvider.Now().Month &&
                                             o.Year == DateTimeProvider.Now().Year
                                                 ? DateTimeProvider.Now().Date
                                                 : o,
                                         Statistics = new[]
                                                          {
                                                              new TransactionStatisticViewModel
                                                                  {
                                                                      BalancePositive =
                                                                          generated.Where(
                                                                              x =>
                                                                              x.Sum >= 0 &&
                                                                              x.Date.Value.Month == o.Month &&
                                                                              x.Date.Value.Year == o.Year).Sum(
                                                                                  x => x.Sum),
                                                                      BalanceNegative =
                                                                          generated.Where(
                                                                              x =>
                                                                              x.Sum < 0 && x.Date.Value.Month == o.Month &&
                                                                              x.Date.Value.Year == o.Year).Sum(
                                                                                  x => x.Sum),
                                                                      CurrencyId = Convert.ToInt32(currency.Value),
                                                                      CurrencyName = currency.Text,
                                                                      CurrencySymbol = currency.Text,
                                                                  }
                                                          }
                                     });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            decimal total = 0;
            foreach (var item in model.Items)
            {
                var stat = item.Statistics.First();
                stat.Balance = stat.BalanceNegative + stat.BalancePositive;
                total += stat.Balance;
                stat.BalanceAccumulative = total;
            }

            model.TargetCurrency = new IdWithSelectList { Id = model.TargetCurrency.Id, Items = GetCurrenciesList(model.TargetCurrency.Id) };

            return View(model);
        }

        [HttpGet]
        public ActionResult ViewGenerated(int id)
        {
            var transaction = PlannedTransactionRepository.GetById(id);
            if (transaction == null)
                return new HttpNotFoundResult();

            if (!MembershipHelper.UsersInOneSection(MembershipHelper.GetUser().Id, transaction.CreatedBy))
                return new HttpNotFoundResult();

            var options = new PlannedTransactionSearchOptions
            {
                Date1 = DateTimeProvider.Now(DateTimeKind.Utc).Date,
                Date2 = DateTimeProvider.Now(DateTimeKind.Utc).Date.AddMonths(7),
            };

            var generated = PlannedTransactionCalculator.GenerateActualByPlannedTransaction(CurrentUser.SectionId, options.Date1.Value, options.Date2.Value, transaction.Id).ToList();

            return View(generated);
        }


        private PlannedTransactionsListViewModel GetModel(PlannedTransactionsSearchViewModel search, DateTime? applyDate = null)
        {
            var model = new PlannedTransactionsListViewModel();

            IEnumerable<PlannedTransaction> transactions =
                applyDate.HasValue ?
                    PlannedTransactionCalculator.FindPlansRelatedToDate(CurrentUser.SectionId, applyDate.Value, new DateTime(applyDate.Value.Year, applyDate.Value.Month, 1).AddMonths(1).AddDays(-1)) :
                    PlannedTransactionRepository.GetList(CurrentUser.SectionId, new PlannedTransactionSearchOptions());

            model.Items = ConvertPlannedTransactionToListViewModel(transactions);

            SearchLastUsed = search;

            return model;
        }

        private IEnumerable<PlannedTransaction> FilterByApplyDate(IEnumerable<PlannedTransaction> transactions, DateTime applyDate1, DateTime applyDate2)
        {
            return PlannedTransactionCalculator.FindPlansRelatedToDate(CurrentUser.SectionId, applyDate1, applyDate2);
        }

        private PlannedTransactionEditViewModel ConvertPlannedTransactionToModel(PlannedTransaction transaction)
        {
            var model =
                new PlannedTransactionEditViewModel
                {
                    Id = transaction.Id,
                    Category = new IdWithSelectList { Id = transaction.CategoryId ?? 0 },
                    TransactionType = new IdWithSelectList { Id = transaction.TransactionTypeId },
                    PeriodType = new IdWithSelectList { Id = transaction.PeriodTypeId },
                    Amount = transaction.Amount,
                    Sum = new CurrencyBundle
                    {
                        Sum = Math.Abs(transaction.Sum),
                        Formula = Math.Abs(transaction.Sum).ToString(CultureInfo.InvariantCulture),
                        Currency = new IdWithSelectList { Id = transaction.CurrencyId },
                        Account = new IdWithSelectList { Id = transaction.AccountId ?? 0 },
                    },
                    FromDate = transaction.FromDate,
                    ToDate = transaction.ToDate,
                    Tags = transaction.Tags,
                    Description = transaction.Description,
                    IsDisabled = transaction.IsDisabled,
                    CreatedBy = transaction.CreatedBy ?? 0,
                    CreatedWhen = transaction.CreatedWhen,
                    UpdatedBy = transaction.UpdatedBy,
                    UpdatedWhen = transaction.UpdatedWhen,

                };

            return model;
        }

        private PlannedTransaction ConvertModelToPlannedTransaction(PlannedTransactionEditViewModel model)
        {
            var type = (TransactionTypes)model.TransactionType.Id;

            return new PlannedTransaction
            {
                Id = model.Id,
                SectionId = CurrentUser.SectionId,
                PeriodTypeId = model.PeriodType.Id,
                Amount = model.Amount,
                TransactionTypeId = model.TransactionType.Id,
                CategoryId = model.Category.Id,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                Description = model.Description,
                IsDisabled = model.IsDisabled,
                Tags = TransactionDataHelper.NormalizeTags(model.Tags),
                CreatedBy = CurrentUser.Id,
                CreatedWhen = DateTime.UtcNow,
                UpdatedWhen = DateTime.UtcNow,
                UpdatedBy = CurrentUser.Id,

                CurrencyId = model.Sum.Currency.Id,
                AccountId = model.Sum.Account.Id,
                Sum = TransactionDataHelper.GetRealSumValue(type, model.Sum.Sum),

            };
        }


        private IEnumerable<PlannedTransactionsListItemViewModel> ConvertPlannedTransactionToListViewModel(IEnumerable<PlannedTransaction> plannedTransactions)
        {
            var list = plannedTransactions.ToList();
            foreach (var plannedTransaction in list)
            {
                var model = new PlannedTransactionsListItemViewModel
                                {
                                    Id = plannedTransaction.Id,
                                    PeriodType = plannedTransaction.PeriodTypeName,
                                    FromDate = plannedTransaction.FromDate,
                                    ToDate = plannedTransaction.ToDate,
                                    Amount = plannedTransaction.Amount,
                                    Tags = plannedTransaction.Tags,
                                    IsDisabled = plannedTransaction.IsDisabled,
                                    Description = plannedTransaction.Description,
                                    CreatedWhen = plannedTransaction.CreatedWhen,
                                    UpdatedWhen = plannedTransaction.UpdatedWhen,

                                    Currency = plannedTransaction.CurrencySymbol,

                                    Account = plannedTransaction.AccountName,

                                    Sum = plannedTransaction.Sum,
                                };

                PopulateCommands(model);

                yield return model;
            }
        }

        private void PopulateCommands(PlannedTransactionsListItemViewModel transaction)
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
			               				IsDefault = false,
			               				Text = "Рассчитать план",
			               				IconCssClass = "cus-page-edit",
			               				Command = new JsViewCommandModel{ClientFunction = "showPlanByRecord", Data = transaction.Id.ToString(CultureInfo.CurrentUICulture)}
			               			},
			               		new ViewCommandUIModel
			               			{
										IsDividerBefore = true,
			               				IsDefault = false,
			               				Text = "Удалить",
			               				IconCssClass = "cus-page-edit",
			               				Command = new JsViewCommandModel{ClientFunction = "deleteRecord", Data = transaction.Id.ToString(CultureInfo.CurrentUICulture)}
			               			},			               	
							};

            transaction.Commands = commands;
        }

        private void PopulateListVariablesInEditViewModel(PlannedTransactionEditViewModel model)
        {
            model.Category = new IdWithSelectList { Id = model.Category.Id, Items = GetCategoriesList(model.Category.Id) };
            model.TransactionType = new IdWithSelectList { Id = model.TransactionType.Id, Items = GetTransactionTypes(model.TransactionType.Id) };

            model.Sum.Account = new IdWithSelectList { Id = model.Sum.Account.Id, Items = GetAccountsList(model.Sum.Account.Id) };
            model.Sum.Currency = new IdWithSelectList { Id = model.Sum.Currency.Id, Items = GetCurrenciesList(model.Sum.Currency.Id) };

            model.PeriodType = new IdWithSelectList { Id = model.PeriodType.Id, Items = GetPeriodTypesList(model.PeriodType.Id) };
        }


        private SelectItemsModel GetPeriodTypesList(int defaultId = 0)
        {
            var items = Dictionaries.PeriodTypes()
                .Where(o => !o.IsDisabled)
                .Select(o => new SelectItemModel { Selected = o.Id.Equals(defaultId) || (defaultId == 0 && o.IsDefault), Text = o.Name, Value = o.Id.ToString() })
                .ToList();

            return new SelectItemsModel(items);
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
                    new SelectListItem
                    {
                        Text = item.GetDisplayAttr().Name,
                        Value = ((int)item).ToString(CultureInfo.InvariantCulture),
                        //Selected = defaultId == (int)item
                    };

            return DictionaryHelper.GetDictionary(() => items,
                o => o.Value,
                o => o.Text,
                o => true,
                defaultId.ToString(CultureInfo.CurrentUICulture));
        }

        private SelectItemsModel GetCurrenciesList(int defaultId = 0)
        {
            var items = Dictionaries.Currencies()
                .Where(o => !o.IsDisabled)
                .Select(o => new SelectItemModel { Selected = o.Id.Equals(defaultId) || (defaultId == 0 && o.IsDefault), Text = o.Name, Value = o.Id.ToString() })
                .ToList();

            return new SelectItemsModel(items);
        }

    }
}
