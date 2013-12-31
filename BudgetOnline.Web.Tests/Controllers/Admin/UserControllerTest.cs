using System;
using System.Globalization;
using System.Web.Mvc;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Areas.Admin.Controllers;
using BudgetOnline.Web.Areas.Admin.Models;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Controllers.Admin
{
	[TestClass]
	public class UsersControllerTest : BaseControllerTest
	{
		private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
		private readonly Mock<IUserPermissionRepository> _userPermissionRepositoryMock = new Mock<IUserPermissionRepository>();
		private readonly Mock<IPermissionRepository> _permissionRepositoryMock = new Mock<IPermissionRepository>();
		private readonly Mock<MembershipHelper> _membershipHelper = new Mock<MembershipHelper>();

		private User _user;
		private User _userFromOtherSection;
		private UserPermission[] _userPermissions;


		protected override void Setup()
		{
			_user = new User
			{
				Id = 1,
				SectionId = 1,
				Name = "User1",
				CreatedWhen = DateTime.UtcNow,
				IsDisabled = false,
			};

			_userFromOtherSection = new User
			{
				Id = 2,
				SectionId = 2,
				Name = "XXX"
			};

			_userPermissions = new[]
			                   	{
			                   		new UserPermission {Id = 1, UserId = _user.Id, PermissionId = Roles.SectionAdmin.GetRealId()},
			                   		new UserPermission {Id = 2, UserId = _user.Id, PermissionId = Roles.FactAdd.GetRealId()},
			                   		new UserPermission {Id = 3, UserId = _user.Id, PermissionId = Roles.FactView.GetRealId()}
			                   	};

			SetupUserRepository();
			SetupPermissionRepositoryMock();
			SetupUserPermissionRepositoryMock();
			SetupMembershipHelper();
		}

		[TestMethod]
		public void EditView_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntExist()
		{
			var controller = GetUserController();

			var result = controller.Edit(-1);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		//[TestMethod]
		//public void EditView_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntBelongsToSameSection()
		//{
		//    var controller = GetUserController();

		//    var result = controller.Edit(_userFromOtherSection.Id);

		//    Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		//}


		[TestMethod]
		public void EditView_ShouldReturnView_WhenCorrectUserRequested()
		{
			var controller = GetUserController();

			var result = controller.Edit(_user.Id);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(UserEditViewModel));

			var model = ((ViewResult)result).Model as UserEditViewModel;

			Assert.IsNotNull(model, "Model shouldn't be null");
			Assert.AreEqual(_user.Name, model.Name, "Name should be equal");
			Assert.AreEqual(_user.Id, model.Id, "Id should be equal");
			Assert.AreEqual(_user.IsDisabled, model.IsDisabled, "IsDisabled should be equal");
		}

		[TestMethod]
		public void EditView_ShouldSaveUserToRepository_WhenCorrectUserPosted()
		{
			var controller = GetUserController();

			var model = new UserEditViewModel { Id = 1, Name = "User", IsDisabled = true, };

			var result = controller.Edit(model);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			Assert.IsTrue(((RedirectToRouteResult)result).RouteValues.Count > 0);

			var user = new User
							{
								Id = model.Id,
								Name = model.Name,
								SectionId = CurrentUser.SectionId,
								IsDisabled = model.IsDisabled,
							};

			//_userRepositoryMock.Verify(o => o.Update(It.IsAny<User>()), Times.Once(), "Should call Update method");
		}


		[TestMethod]
		public void CreatePost_ShouldInsertPermissionToRepository_WhenFActPermissionsAdded()
		{
			SetupUserPermissionRepositoryMockForNewUser();

			var controller = GetUserController();

			var initialModel = new UserEditViewModel
			{
				Id = _user.Id,
				Name = _user.Name,
				IsDisabled = _user.IsDisabled,
				Permissions = new ListWithMultiSelects
								{
									Items = new[] {
										new SelectListItem{ Selected = true, Text = "", Value = Roles.FactAdd.GetRealId().ToString(CultureInfo.InvariantCulture)},
										new SelectListItem{ Selected = true, Text = "", Value = Roles.FactView.GetRealId().ToString(CultureInfo.InvariantCulture)},
									}
								}
			};

			controller.Create(initialModel);

			_userPermissionRepositoryMock.Verify(o => o.Insert(It.IsAny<UserPermission>()), Times.Exactly(2));

			_userPermissionRepositoryMock.Verify(
				o => o.Insert(It.Is<UserPermission>(
					t => t.UserId == initialModel.Id
						&& t.PermissionId == Roles.FactAdd.GetRealId()
						&& t.Id == 0
					)), Times.Once());

			_userPermissionRepositoryMock.Verify(
				o => o.Insert(It.Is<UserPermission>(
					t => t.UserId == initialModel.Id
						&& t.PermissionId == Roles.FactView.GetRealId()
						&& t.Id == 0
					)), Times.Once());
		}

		[TestMethod]
		public void EditPost_ShouldInsertPermissionToRepository_WhenNewPermissionsAdded()
		{
			var controller = GetUserController();

			var initialModel = new UserEditViewModel
			{
				Id = _user.Id,
				Name = _user.Name,
				IsDisabled = _user.IsDisabled,
				Permissions = new ListWithMultiSelects
				{
					Items = new[] {
										new SelectListItem{ Selected = true, Text = "", Value = Roles.SectionAdmin.GetRealId().ToString(CultureInfo.InvariantCulture)},
										new SelectListItem{ Selected = true, Text = "", Value = Roles.FactAdd.GetRealId().ToString(CultureInfo.InvariantCulture)},
										new SelectListItem{ Selected = true, Text = "", Value = Roles.FactView.GetRealId().ToString(CultureInfo.InvariantCulture)},
										new SelectListItem{ Selected = true, Text = "", Value = Roles.PlanAdd.GetRealId().ToString(CultureInfo.InvariantCulture)},
									}
				}
			};

			controller.Edit(initialModel);

			_userPermissionRepositoryMock.Verify(o => o.Insert(It.IsAny<UserPermission>()), Times.Exactly(1));

			_userPermissionRepositoryMock.Verify(
				o => o.Insert(It.Is<UserPermission>(
					t => t.UserId == initialModel.Id
						&& t.PermissionId == Roles.PlanAdd.GetRealId()
						&& t.Id == 0
					)), Times.Once());
		}


		[TestMethod]
		public void EditPost_ShouldDeletePermissionFromRepository_WhenPermissionRemoved()
		{
			var controller = GetUserController();

			var initialModel = new UserEditViewModel
			{
				Id = _user.Id,
				Name = _user.Name,
				IsDisabled = _user.IsDisabled,
				Permissions = new ListWithMultiSelects
				{
					Items = new[] {
										new SelectListItem{ Selected = true, Text = "", Value = Roles.FactAdd.GetRealId().ToString(CultureInfo.InvariantCulture)},
										new SelectListItem{ Selected = true, Text = "", Value = Roles.FactView.GetRealId().ToString(CultureInfo.InvariantCulture)},
									}
				}
			};

			controller.Edit(initialModel);

			_userPermissionRepositoryMock.Verify(o => o.Insert(It.IsAny<UserPermission>()), Times.Never());

			_userPermissionRepositoryMock.Verify(
				o => o.Delete(It.Is<int>(
					t => t == _userPermissions[0].Id
					)), Times.Once());
		}

		private UsersController GetUserController()
		{
			var controller = new UsersController();
			controller.MembershipHelper = MembershipHelperMock.Object;
			controller.CacheWrapper = CacheWrapperMock.Object;
			controller.UserRepository = _userRepositoryMock.Object;
			controller.UserPermissionRepository = _userPermissionRepositoryMock.Object;
			controller.PermissionRepository = _permissionRepositoryMock.Object;
			controller.MembershipHelper = _membershipHelper.Object;
			return controller;
		}


		private void SetupUserRepository()
		{
			//_userRepositoryMock
			//    .Setup(o => o.GetList(It.IsAny<int>()))
			//    .Returns(new[]
			//                {
			//                    _user,
			//                    _userFromOtherSection,
			//                }.AsQueryable());

			_userRepositoryMock
				.Setup(o => o.Insert(It.IsAny<User>()))
				.Returns(_user);

			_userRepositoryMock
				.Setup(o => o.GetUser(It.Is<int>(i => i == _user.Id)))
				.Returns(_user);

			_userRepositoryMock
				.Setup(o => o.GetUser(It.Is<int>(i => i == _userFromOtherSection.Id)))
				.Returns(_userFromOtherSection);
		}

		private void SetupUserPermissionRepositoryMock()
		{
			_userPermissionRepositoryMock
				.Setup(o => o.GetPermissions(It.Is<int>(i => i == _user.Id)))
				.Returns(_userPermissions);
		}

		private void SetupUserPermissionRepositoryMockForNewUser()
		{
			_userPermissionRepositoryMock
				.Setup(o => o.GetPermissions(It.Is<int>(i => i == _user.Id)))
				.Returns(new UserPermission[0]);
		}

		private void SetupPermissionRepositoryMock()
		{
			_permissionRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(new[]
				         	{
				         		new Permission{ Id = Roles.SystemAdmin.GetRealId(), Name = "SystemAdmin"},
								new Permission{ Id = Roles.SectionAdmin.GetRealId(), Name = "SectionAdmin"},

				         		new Permission{ Id = Roles.FactAdd.GetRealId(), Name = "FactAdd"},
								new Permission{ Id = Roles.FactView.GetRealId(), Name = "FactView"},

						        new Permission{ Id = Roles.PlanAdd.GetRealId(), Name = "PlanAdd"},
								new Permission{ Id = Roles.PlanView.GetRealId(), Name = "PlanView"}
							});
		}

		private void SetupMembershipHelper()
		{
			_membershipHelper
				.Setup(o => o.GetUser())
				.Returns(new UserModel
				{
					Id = _user.Id,
					Name = _user.Name,
					Email = _user.Email
				});

			_membershipHelper
				.Setup(o => o.GetUser(_user.Id))
				.Returns(new UserModel
				{
					Id = _user.Id,
					Name = _user.Name,
					Email = _user.Email
				});
		}
	}
}
