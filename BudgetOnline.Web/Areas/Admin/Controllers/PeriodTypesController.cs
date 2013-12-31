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
using BudgetOnline.Web.Models;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Areas.Admin.Controllers
{
    public class PeriodTypesController : ListController
    {
        public IPeriodTypeRepository PeriodTypeRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }

        [HttpGet]
        public ActionResult List()
        {
            return View(GetData());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var PeriodType = PeriodTypeRepository.Get(id);

            //if (PeriodType == null || !IsSectionValid(PeriodType, o => o.SectionId))
            //{
            //    return HttpNotFound();
            //}

            var model = Mapper.DynamicMap<PeriodType, PeriodTypeEditViewModel>(PeriodType);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PeriodTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var PeriodType = Mapper.DynamicMap<PeriodTypeEditViewModel, PeriodType>(model);
                //PeriodType.UpdatedBy = MembershipHelper.CurrentUser.Id;

                PeriodTypeRepository.Update(PeriodType);

                Dictionaries.ResetPeriodTypes();

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PeriodTypeEditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PeriodTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var periodType = Mapper.DynamicMap<PeriodTypeEditViewModel, PeriodType>(model);
                //PeriodType.CreatedWhen = DateTime.UtcNow;
                //PeriodType.CreatedBy = MembershipHelper.CurrentUser.Id;
                //PeriodType.UpdatedBy = null;
                //PeriodType.UpdatedWhen = null;
                //PeriodType.SectionId = MembershipHelper.CurrentUser.SectionId;

                Dictionaries.ResetPeriodTypes();

                PeriodTypeRepository.Insert(periodType);

                return RedirectToAction("list");
            }

            return View(model);
        }

        private IEnumerable<PeriodTypeListViewModel> GetData()
        {
            var items = PeriodTypeRepository
                .GetList(MembershipHelper.CurrentUser.SectionId)
                .Select(o => new PeriodTypeListViewModel
                    {
                        Id = o.Id,
                        Name = o.Name,
                        IsDisabled = o.IsDisabled,
                        //CreatedWhen = o.CreatedWhen,
                        //UpdatedWhen = o.UpdatedWhen,
                    })
                .AsEnumerable();

            items = PopulateListCommands(items);

            return items;
        }

        private IEnumerable<PeriodTypeListViewModel> PopulateListCommands(IEnumerable<PeriodTypeListViewModel> items)
        {
            var currentUser = MembershipHelper.GetUser();

            foreach (var item in items)
            {
                var listOfCommands = new List<ViewCommandUIModel> { CreateEditCommand(item.Id) };

                item.Commands = listOfCommands;

                yield return item;
            }
        }

        private ViewCommandUIModel CreateEditCommand(int recordId)
        {
            var post = new RedirectViewCommandModel
            {
                Path = Url.Action("edit", "PeriodTypes", new { area = "Admin", id = recordId }),
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
    }
}
