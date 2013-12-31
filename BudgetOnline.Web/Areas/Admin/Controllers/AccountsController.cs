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
using BudgetOnline.Web.Infrastructure.Core;

namespace BudgetOnline.Web.Areas.Admin.Controllers
{
    public class AccountsController : ListController
    {
        public IAccountRepository AccountRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }

        [HttpGet]
        public ActionResult List()
        {
            return View(GetData());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var account = AccountRepository.Get(id);

            if (account == null || !IsSectionValid(account, o => o.SectionId))
            {
                return HttpNotFound();
            }

            var model = Mapper.DynamicMap<Account, AccountEditViewModel>(account);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = Mapper.DynamicMap<AccountEditViewModel, Account>(model);
                account.UpdatedBy = MembershipHelper.CurrentUser.Id;
                if (account.IsDefault && account.IsDisabled)
                    account.IsDefault = false;

                if (account.IsDefault)
                    RemoveOldDefault(account.Id);

                AccountRepository.Update(account);

                Dictionaries.ResetAccounts();

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AccountEditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = Mapper.DynamicMap<AccountEditViewModel, Account>(model);
                account.CreatedWhen = DateTime.UtcNow;
                account.CreatedBy = MembershipHelper.CurrentUser.Id;
                account.UpdatedBy = null;
                account.UpdatedWhen = null;
                account.SectionId = MembershipHelper.CurrentUser.SectionId;

                if (account.IsDefault)
                    RemoveOldDefault(account.Id);

                AccountRepository.Insert(account);

                Dictionaries.ResetAccounts();

                return RedirectToAction("list");
            }

            return View(model);
        }

        private IEnumerable<AccountListViewModel> GetData()
        {
            var items = AccountRepository
                .GetList(MembershipHelper.CurrentUser.SectionId)
                .Select(o => new AccountListViewModel
                    {
                        Id = o.Id,
                        Name = o.Name,
                        IsDisabled = o.IsDisabled,
                        CreatedWhen = o.CreatedWhen,
                        UpdatedWhen = o.UpdatedWhen,
                        IsDefault = o.IsDefault
                    }).AsEnumerable();

            items = PopulateListCommands(items);

            return items;
        }

        private IEnumerable<AccountListViewModel> PopulateListCommands(IEnumerable<AccountListViewModel> items)
        {
            foreach (var item in items)
            {
                var listOfCommands = new List<ViewCommandUIModel> { CreateEditCommand(item.Id), };

                item.Commands = listOfCommands;

                yield return item;
            }
        }

        private ViewCommandUIModel CreateEditCommand(int recordId)
        {
            var post = new RedirectViewCommandModel
            {
                Path = Url.Action("edit", "Accounts", new { area = "Admin", id = recordId }),
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

        private void RemoveOldDefault(int accountId)
        {
            var defaultAccount = AccountRepository.GetDefault(MembershipHelper.CurrentUser.SectionId);
            if (defaultAccount != null && defaultAccount.Id != accountId)
            {
                defaultAccount.IsDefault = false;
                AccountRepository.Update(defaultAccount);
            }
        }
    }
}
