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
    public class CategoriesController : ListController
    {
        public ICategoryRepository CategoryRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }

        [HttpGet]
        public ActionResult List()
        {
            return View(GetData());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = CategoryRepository.Get(id);

            if (category == null || !IsSectionValid(category, o => o.SectionId))
            {
                return HttpNotFound();
            }

            var model = Mapper.DynamicMap<Category, CategoryEditViewModel>(category);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.DynamicMap<CategoryEditViewModel, Category>(model);
                category.UpdatedBy = MembershipHelper.CurrentUser.Id;
                if (category.IsDefault && category.IsDisabled)
                    category.IsDefault = false;

                if (category.IsDefault)
                    RemoveOldDefault(category.Id);

                CategoryRepository.Update(category);

                Dictionaries.ResetCategories();

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CategoryEditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.DynamicMap<CategoryEditViewModel, Category>(model);
                category.CreatedWhen = DateTime.UtcNow;
                category.CreatedBy = MembershipHelper.CurrentUser.Id;
                category.UpdatedBy = null;
                category.UpdatedWhen = null;
                category.SectionId = MembershipHelper.CurrentUser.SectionId;

                if (category.IsDefault)
                    RemoveOldDefault(category.Id);

                CategoryRepository.Insert(category);

                Dictionaries.ResetCategories();

                return RedirectToAction("list");
            }

            return View(model);
        }

        private IEnumerable<CategoryListViewModel> GetData()
        {
            var items = CategoryRepository
                .GetList(MembershipHelper.CurrentUser.SectionId)
                .Select(o => new CategoryListViewModel
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

        private IEnumerable<CategoryListViewModel> PopulateListCommands(IEnumerable<CategoryListViewModel> items)
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
                Path = Url.Action("edit", "Categories", new { area = "Admin", id = recordId }),
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

        private void RemoveOldDefault(int categoryId)
        {
            var defaultCategory = CategoryRepository.GetDefault(MembershipHelper.CurrentUser.SectionId);
            if (defaultCategory != null && defaultCategory.Id != categoryId)
            {
                defaultCategory.IsDefault = false;
                CategoryRepository.Update(defaultCategory);
            }
        }
    }
}
