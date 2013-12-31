using System;
using System.Linq;
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
	public class AccountControllerTest
	{
		private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
		private readonly Mock<MembershipHelper> _membershipHelper = new Mock<MembershipHelper>();

		private const int SectionId = 1;
		private Account _account;
		private Account _accountFromOtherSection;

		private readonly UserModel _currentUser =
			new UserModel
			{
				Id = 1,
				SectionId = SectionId,
			};
		private readonly UserModel _createdUser =
			new UserModel
			{
				Id = 2,
				SectionId = SectionId,
			};
		private readonly UserModel _createdUserFromOtherSection =
			new UserModel
			{
				Id = 3,
				SectionId = SectionId + SectionId,
			};

		[TestInitialize]
		public void Setup()
		{
			_account = new Account
			{
				Id = 1,
				Name = "Account1",
				Description = "Description for Account1",
				IsDisabled = true,
				IsDefault = false,
				CreatedBy = _createdUser.Id,
				CreatedWhen = DateTime.UtcNow,
			};


			_accountFromOtherSection = new Account
			{
				Id = 2,
				Name = "Account2",
				Description = "Description for Account2",
				CreatedBy = _createdUserFromOtherSection.Id,
			};

			_accountRepositoryMock
				.Setup(o => o.GetList(It.Is<int>(p => p == SectionId)))
				.Returns(new[] { _account }.AsQueryable());

			_accountRepositoryMock
				.Setup(o => o.GetList(It.Is<int>(p => p != SectionId)))
				.Returns(new[] { _accountFromOtherSection }.AsQueryable());

			_membershipHelper
				.Setup(o => o.GetUser())
				.Returns(_currentUser);

			_membershipHelper
				.Setup(o => o.UsersInOneSection(It.Is<int?[]>(p => p.Length == 2 && p[0] == _currentUser.Id && p[1] == _createdUser.Id)))
				.Returns(true);
		}

		[TestMethod]
		public void CreateView_ShouldNotPassValidation_WhenTagsEmpty()
		{
			var controller = GetAccountController();

			var initialModel = GetSimpleCreateModel();
			initialModel.Name = null;
			controller.ModelState.AddModelError("Name", "Name is empty");

			var result = controller.Create(initialModel);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(AccountEditViewModel));

			AssertAfterCreateFailed(initialModel, ((ViewResult)result).Model as AccountEditViewModel);
		}

		private AccountsController GetAccountController()
		{
			var controller = new AccountsController
								{
									MembershipHelper = _membershipHelper.Object,
									AccountRepository = _accountRepositoryMock.Object,
								};

			return controller;
		}

		private AccountEditViewModel GetSimpleCreateModel()
		{
			return new AccountEditViewModel
			{
				Id = 0,
				Name = "Account",
				Description = "Description",
				ShowForIncome = true,
				ShowForOutcome = true,
				ShowForTransfer = true,
				IsDefault = false,
				IsDisabled = false,
			};
		}

		private void AssertAfterCreateFailed(AccountEditViewModel sourceModel, AccountEditViewModel resultModel)
		{
			Assert.AreEqual(sourceModel.Name, resultModel.Name, "Name should be equal");
			Assert.AreEqual(sourceModel.Description, resultModel.Description, "Description should be equal");
			Assert.AreEqual(0, resultModel.Id, "Id should be zero");
			//Assert.AreEqual(sourceModel.Date, resultModel.Date, "Date should be equal");
			Assert.AreEqual(sourceModel.IsDisabled, resultModel.IsDisabled, "IsDisabled should be equal");
			Assert.AreEqual(sourceModel.IsDefault, resultModel.IsDefault, "IsDefault should be equal");
			Assert.AreEqual(sourceModel.ShowForIncome, resultModel.ShowForIncome, "ShowForIncome should be equal");
			Assert.AreEqual(sourceModel.ShowForOutcome, resultModel.ShowForOutcome, "ShowForOutcome should be equal");
			Assert.AreEqual(sourceModel.ShowForTransfer, resultModel.ShowForTransfer, "ShowForTransfer should be equal");
		}
	}
}
