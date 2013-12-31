using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Models.ViewCommands;
using BudgetOnline.Web.Areas.Admin.Models;
using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Infrastructure.Binders;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;

namespace BudgetOnline.Web.Areas.Admin.Controllers
{
    public class UsersController : ListWithSearchController<UserListSearchViewModel>
    {
        public IUserRepository UserRepository { get; set; }
        public IUserConnectRepository UserConnectRepository { get; set; }
        public IUserPermissionRepository UserPermissionRepository { get; set; }
        public IPermissionRepository PermissionRepository { get; set; }
        public ILogWriter Log { get; set; }

        protected override string SearchCacheKey { get { return "users_search_cache_key"; } }

        [HttpGet]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult List(string searchBy)
        {
            return View(
                GetModel(new UserListSearchViewModel { Text = searchBy }));
        }

        [HttpPost]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult List(FormCollection form)
        {
            var searchBy = form["searchBy"];
            Log.TraceFormat("User search criteria. SearchBy={0}", searchBy);



            //var items = UserConnectRepository
            //    .GetUsersWithConnects(MembershipHelper.GetUser().SectionId, searchBy)
            //    .Select(o => new UserListItemsViewModel
            //    {
            //        Email = o.Email,
            //        Name = o.Name,
            //        Id = o.UserId,
            //        IsBocked = o.IsDisabled,
            //        IsAdmin = o.IsSectionAdmin,
            //        WhenLastConnected = o.LastConnected
            //    });

            //items = PopulateListCommands(items);

            //var search = new SearchCriteria<UserListItemsViewModel>
            //{
            //    Key = SearchCacheKey,
            //    Result = items
            //};

            //SearchCriteria = search;


            return RedirectToAction("list", new { searchBy });
        }

        [HttpGet]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Create()
        {
            var model = new UserEditViewModel();

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Create(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = MembershipHelper.GetUser();

                var user = new User
                {
                    Id = 0,
                    Email = model.Email,
                    CreatedWhen = DateTime.UtcNow,
                    ContactPhoneNumber = model.ContactPhoneNumber,
                    Name = model.Name,
                    SectionId = currentUser.SectionId
                };

                user = UserRepository.Insert(user);

                UpdateUserPermissions(user.Id, model.Permissions.Items);

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Edit(int id)
        {
            var user = MembershipHelper.GetUser(id);
            if (user == null)
                return new HttpNotFoundResult();

            var userPermissions = UserPermissionRepository.GetPermissions(user.Id);

            var model = new UserEditViewModel
                            {
                                Id = user.Id,
                                Email = user.Email,
                                Name = user.Name,
                                IsDisabled = user.IsDisabled,
                                IsSectionAdmin = user.IsHasPermission(Roles.SectionAdmin),
                                SectionId = user.SectionId,
                                Permissions = new ListWithMultiSelects
                                {
                                    Items = userPermissions.Select(o => new SelectListItem { Selected = true, Value = o.PermissionId.ToString(CultureInfo.InvariantCulture) })
                                }
                            };

            PopulateListOfPermissions(model);

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Edit([ModelBinder(typeof(CustomViewModelBinder))]UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                            {
                                Id = model.Id,
                                Email = model.Email,
                                UpdatedWhen = DateTime.UtcNow,
                                ContactPhoneNumber = model.ContactPhoneNumber,
                                Name = model.Name,
                            };

                UserRepository.Update(user);

                UpdateUserPermissions(user.Id, model.Permissions.Items);

                return RedirectToAction("list");
            }

            PopulateListOfPermissions(model);

            return View(model);
        }

        [HttpGet]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Disable(int id)
        {
            var user = UserRepository.GetUser(id);

            var model = new EnableDisableUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsDisabled = user.IsDisabled
            };

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Disable(EnableDisableUserViewModel model)
        {
            var user = UserRepository.GetUser(model.Id);

            if (ModelState.IsValid)
            {
                var changedUser = UserRepository.GetUser(user.Id);
                changedUser.IsDisabled = true;
                UserRepository.Update(changedUser);

                return RedirectToAction("list");
            }

            return View(model);
        }

        [HttpGet]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Enable(int id)
        {
            var user = UserRepository.GetUser(id);

            var model = new EnableDisableUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsDisabled = user.IsDisabled
            };

            return View(model);
        }

        [HttpPost]
        [RestrictRole(Roles.SectionAdmin)]
        public ActionResult Enable(EnableDisableUserViewModel model)
        {
            var user = UserRepository.GetUser(model.Id);

            if (ModelState.IsValid)
            {
                var changedUser = UserRepository.GetUser(user.Id);
                changedUser.IsDisabled = false;
                UserRepository.Update(changedUser);

                return RedirectToAction("list");
            }

            return View(model);
        }

        private UserListViewModel GetModel(UserListSearchViewModel search)
        {
            var model = new UserListViewModel();

            var items = UserConnectRepository
                .GetUsersWithConnects(MembershipHelper.GetUser().SectionId, search.Text)
                .Select(o => new UserListItemsViewModel
                {
                    Email = o.Email,
                    Name = o.Name,
                    Id = o.UserId,
                    IsBocked = o.IsDisabled,
                    IsAdmin = o.IsSectionAdmin,
                    WhenLastConnected = o.LastConnected
                });

            model.Items = PopulateListCommands(items);
            model.Search = search;

            SearchLastUsed = search;

            return model;
        }

        private void UpdateUserPermissions(int userId, IEnumerable<SelectListItem> items)
        {
            var parsedPermissions = items.Select(o => o.Value).ToList();
            var userPemissions = UserPermissionRepository.GetPermissions(userId);

            foreach (var userPermission in userPemissions)
            {
                string permissionId = userPermission.PermissionId.ToString(CultureInfo.InvariantCulture);

                if (!parsedPermissions.Contains(permissionId))
                    UserPermissionRepository.Delete(userPermission.Id);
                else
                    parsedPermissions.Remove(permissionId);
            }

            if (parsedPermissions.Any())
            {
                var permissions = PermissionRepository.GetList(CurrentUser.SectionId).ToList();
                foreach (var parsedPermission in parsedPermissions)
                {
                    int id;
                    if (int.TryParse(parsedPermission, out id))
                        if (permissions.Any(o => o.Id == id))
                            UserPermissionRepository.Insert(
                                new UserPermission
                                {
                                    CreatedBy = CurrentUser.Id,
                                    CreatedWhen = DateTime.UtcNow,
                                    PermissionId = id,
                                    UserId = userId
                                });
                }
            }
        }

        private void PopulateListOfPermissions(UserEditViewModel model)
        {
            var permissions = PermissionRepository.GetList(CurrentUser.SectionId).ToList();
            var multiSelectList = new ListWithMultiSelects
            {
                Items = permissions
                            .Select(o => new SelectListItem
                            {
                                Text = o.Name,
                                Value = o.Id.ToString(CultureInfo.InvariantCulture),
                                Selected = false
                            })
                            .ToList(),
            };

            if (model.Permissions.Items != null)
            {
                foreach (var item in model.Permissions.Items)
                {
                    var found = multiSelectList.Items.FirstOrDefault(o => o.Value == item.Value);
                    if (found != null)
                        found.Selected = true;
                }
            }

            model.Permissions = multiSelectList;
        }

        //private const string SearchCacheKey = "users_search_cache_key";
        //private SearchCriteria<UserListViewModel> SearchCriteria
        //{
        //    get
        //    {
        //        if (CacheWrapper.Exists(SearchCacheKey))
        //            return CacheWrapper.Get<SearchCriteria<UserListViewModel>>(SearchCacheKey);

        //        return null;
        //    }
        //    set
        //    {
        //        CacheWrapper.Put(SearchCacheKey, value, Constants.GetDefaultSearchCacheTimeout);
        //    }
        //}

        private IEnumerable<UserListItemsViewModel> PopulateListCommands(IEnumerable<UserListItemsViewModel> items)
        {
            foreach (var item in items)
            {
                var listOfCommands = new List<ViewCommandUIModel> { CreateEditUserCommand(item.Id) };

                //if (currentUser.IsSectionAdmin && currentUser.Id != item.Id)
                //{
                //    listOfCommands.Add(
                //        !item.IsBocked ?
                //            CreateDisableUserCommand(item.Id) :
                //            CreateEnableUserCommand(item.Id));

                //    if (!item.IsBocked && !item.IsAdmin)
                //        listOfCommands.Add(CreateMakeAdminUserCommand(item.Id));
                //}

                item.Commands = listOfCommands;

                yield return item;
            }
        }

        private ViewCommandUIModel CreateMakeAdminUserCommand(int userId)
        {
            var post = new PostViewCommandModel
            {
                Path = Url.Action("set-admin", "users", new { area = "Admin", id = userId })
            };

            var result = new ViewCommandUIModel
            {
                Title = "",
                Text = "Сделать администратором",
                Command = post,
                IconCssClass = "",
                IsDividerBefore = true,
                IsDefault = false
            };

            return result;
        }

        private ViewCommandUIModel CreateEditUserCommand(int userId)
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

        private ViewCommandUIModel CreateDisableUserCommand(int userId)
        {
            var post = new PostViewCommandModel
            {
                Path = Url.Action("disable", new { id = userId })
            };

            var result = new ViewCommandUIModel
            {
                Title = "",
                Text = "Блокировать",
                Command = post,
                IconCssClass = "icon-ban-circle"
            };

            return result;
        }

        private ViewCommandUIModel CreateEnableUserCommand(int userId)
        {
            var post = new RedirectViewCommandModel
            {
                Path = Url.Action("enable", new { id = userId })
            };

            var result = new ViewCommandUIModel
            {
                Title = "",
                Text = "Разблокировать",
                Command = post,
                IconCssClass = "icon-play-circle"
            };

            return result;
        }


    }
}
