using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.SelectItems;
using BudgetOnline.UI.Models.ViewCommands;
using BudgetOnline.Web.Areas.Admin.Models;
using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Infrastructure.Binders;

namespace BudgetOnline.Web.Areas.Admin.Controllers
{
    public class CurrencyRatesController : ListController
    {
        public ICurrencyRateRepository CurrencyRateRepository { get; set; }
        public ICurrencyRepository CurrencyRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }
        public IMapper Mapper { get; set; }

        [HttpGet]
        public ActionResult List()
        {
            return View(GetData());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var currencyRate = CurrencyRateRepository.Get(id);

            if (currencyRate == null || !IsSectionValid(currencyRate, o => o.SectionId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CurrencyRate, CurrencyRateEditViewModel>(currencyRate);
            model.BaseCurrency.Id = currencyRate.BaseCurrencyId;
            model.TargetCurrency.Id = currencyRate.TargetCurrencyId;

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([ModelBinder(typeof(CustomViewModelBinder))] CurrencyRateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currencyRate = Mapper.Map<CurrencyRateEditViewModel, CurrencyRate>(model);
                currencyRate.UpdatedBy = MembershipHelper.CurrentUser.Id;

                CurrencyRateRepository.Update(currencyRate);

                return RedirectToAction("list");
            }

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CurrencyRateEditViewModel();

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(CustomViewModelBinder))] CurrencyRateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currencyRate = Mapper.Map<CurrencyRateEditViewModel, CurrencyRate>(model);
                currencyRate.CreatedWhen = DateTime.UtcNow;
                currencyRate.CreatedBy = MembershipHelper.CurrentUser.Id;
                currencyRate.UpdatedBy = null;
                currencyRate.UpdatedWhen = null;
                currencyRate.SectionId = MembershipHelper.CurrentUser.SectionId;

                CurrencyRateRepository.Insert(currencyRate);

                return RedirectToAction("list");
            }

            PopulateListVariablesInEditViewModel(model);

            return View(model);
        }

        private IEnumerable<CurrencyRateListViewModel> GetData()
        {
            var items = CurrencyRateRepository
                .GetList(MembershipHelper.CurrentUser.SectionId)
                .Select(o => new CurrencyRateListViewModel
                    {
                        Id = o.Id,
                        Date = o.Date,
                        Rate = o.Rate,
                        BaseCurrencyName = o.BaseCurrencyName,
                        TargetCurrencyName = o.TargetCurrencyName,
                        IsDisabled = o.IsDisabled,
                        CreatedWhen = o.CreatedWhen,
                        UpdatedWhen = o.UpdatedWhen,
                    }).AsEnumerable();

            items = PopulateListCommands(items);

            return items;
        }

        private IEnumerable<CurrencyRateListViewModel> PopulateListCommands(IEnumerable<CurrencyRateListViewModel> items)
        {
            foreach (var item in items)
            {
                var listOfCommands = new List<ViewCommandUIModel> { CreateEditCommand(item.Id) };

                item.Commands = listOfCommands;

                yield return item;
            }
        }

        private ViewCommandUIModel CreateEditCommand(int userId)
        {
            var post = new RedirectViewCommandModel
            {
                Path = Url.Action("edit", new { id = userId })
            };

            var result = new ViewCommandUIModel
            {
                Title = "",
                Text = "Редактировать",
                Command = post,
                IconCssClass = "icon-edit",
                IsDefault = true
            };

            return result;
        }

        private void PopulateListVariablesInEditViewModel(CurrencyRateEditViewModel model)
        {
            var nonDefault = Dictionaries.Currencies().FirstOrDefault(o => o.IsDefault != true);
            var targetDefaultId = nonDefault != null && model.TargetCurrency.Id <= 0
                                      ? nonDefault.Id
                                      : model.TargetCurrency.Id;

            model.BaseCurrency = new IdWithSelectList { Id = model.BaseCurrency.Id, Items = GetCurrenciesList(model.BaseCurrency.Id) };
            model.TargetCurrency = new IdWithSelectList { Id = targetDefaultId, Items = GetCurrenciesList(targetDefaultId) };
        }

        private SelectItemsModel GetCurrenciesList(int defaultId = 0)
        {
            var currencies = Dictionaries.Currencies().ToList();
            if (defaultId <= 0)
            {
                var defaultRecord = currencies.FirstOrDefault(o => o.IsDefault);
                if (defaultRecord != null)
                    defaultId = defaultRecord.Id;
            }

            //return new SelectList(currencies, "Id", "Name", defaultId);
            return new SelectItemsModel(
                currencies.Select(o => new SelectItemModel { Value = o.Id.ToString(), Text = o.Name, Selected = (o.Id == defaultId) }));
        }

    }
}
