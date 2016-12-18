using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Models.ViewCommands;
using BudgetOnline.Web.Areas.Admin.Models;
using BudgetOnline.Web.Controllers;

namespace BudgetOnline.Web.Areas.Admin.Controllers
{
    public class CurrenciesController : ListController
    {
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
            var currency = CurrencyRepository.Get(id);

            if (currency == null || !IsSectionValid(currency, o => o.SectionId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Currency, CurrencyEditViewModel>(currency);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CurrencyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currency = Mapper.Map<CurrencyEditViewModel, Currency>(model);
                currency.UpdatedBy = MembershipHelper.CurrentUser.Id;
                if (currency.IsDefault && currency.IsDisabled)
                    currency.IsDefault = false;

                if (currency.IsDefault)
                    RemoveOldDefault(currency.Id);

                CurrencyRepository.Update(currency);

                Dictionaries.ResetCurrencies();

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CurrencyEditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CurrencyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currency = Mapper.Map<CurrencyEditViewModel, Currency>(model);
                currency.CreatedWhen = DateTime.UtcNow;
                currency.CreatedBy = MembershipHelper.CurrentUser.Id;
                currency.UpdatedBy = null;
                currency.UpdatedWhen = null;
                currency.SectionId = MembershipHelper.CurrentUser.SectionId;

                if (currency.IsDefault)
                    RemoveOldDefault(currency.Id);

                CurrencyRepository.Insert(currency);

                Dictionaries.ResetCurrencies();

                return RedirectToAction("list");
            }

            return View(model);
        }

        private IEnumerable<CurrencyListViewModel> GetData()
        {
            var items = CurrencyRepository
                .GetList(MembershipHelper.CurrentUser.SectionId)
                .Select(o => new CurrencyListViewModel
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Symbol = o.Symbol,
                        IsDisabled = o.IsDisabled,
                        CreatedWhen = o.CreatedWhen,
                        UpdatedWhen = o.UpdatedWhen,
                        IsDefault = o.IsDefault
                    }).AsEnumerable();

            items = PopulateListCommands(items);

            return items;
        }

        private IEnumerable<CurrencyListViewModel> PopulateListCommands(IEnumerable<CurrencyListViewModel> items)
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

        private void RemoveOldDefault(int currencyId)
        {
            var defaultCurrency = CurrencyRepository.GetDefault(MembershipHelper.CurrentUser.SectionId);
            if (defaultCurrency != null && defaultCurrency.Id != currencyId)
            {
                defaultCurrency.IsDefault = false;
                CurrencyRepository.Update(defaultCurrency);
            }
        }
    }
}
