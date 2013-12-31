using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Models.ViewCommands;
using BudgetOnline.Web.Areas.Admin.Models;
using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Models;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Areas.Admin.Controllers
{
	public class TagsController : ListController
    {
        public ITagRepository TagRepository { get; set; }

        [HttpGet]
        public ActionResult List()
        {
            return View(GenerateModel());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Tag = TagRepository.Get(id);

            if (Tag == null || !IsSectionValid(Tag, o => o.SectionId))
            {
                return HttpNotFound();
            }

            var model = Mapper.DynamicMap<Tag, TagEditViewModel>(Tag);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TagEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tag = Mapper.DynamicMap<TagEditViewModel, Tag>(model);
                tag.UpdatedBy = MembershipHelper.CurrentUser.Id;

                TagRepository.Update(tag);

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new TagEditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TagEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tag = Mapper.DynamicMap<TagEditViewModel, Tag>(model);
                tag.CreatedWhen = DateTime.UtcNow;
                tag.CreatedBy = MembershipHelper.CurrentUser.Id;
                tag.UpdatedBy = null;
                tag.UpdatedWhen = null;
                tag.SectionId = MembershipHelper.CurrentUser.SectionId;

                TagRepository.Insert(tag);

                return RedirectToAction("list");
            }

            return View(model);
        }

        private IEnumerable<TagListViewModel> GenerateModel()
        {
            var items = TagRepository
                .GetList(MembershipHelper.CurrentUser.SectionId)
                .Select(o => new TagListViewModel
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Hits = o.Hits,
                        IsDisabled = o.IsDisabled,
                        CreatedWhen = o.CreatedWhen,
                        UpdatedWhen = o.UpdatedWhen,
                    })
                .AsEnumerable();

            items = PopulateListCommands(items);

            return items;
        }

        private IEnumerable<TagListViewModel> PopulateListCommands(IEnumerable<TagListViewModel> items)
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
                Path = Url.Action("edit", "Tags", new { area = "Admin", id = recordId }),
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
