using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Controllers
{
	[TestClass]
	public class SecuredControllerTest
	{
		private readonly Mock<MembershipHelper> _membershipHelper = new Mock<MembershipHelper>();

		private readonly UserModel _currentUser =
			new UserModel
			{
				Id = 1,
				SectionId = 1,
			};

		private readonly UserModel _objInSection =
			new UserModel
			{
				Id = 101,
				SectionId = 1,
			};
		private readonly UserModel _objOutSection =
			new UserModel
			{
				Id = 102,
				SectionId = -1,
			};		

		[TestInitialize]
		public void Setup()
		{
			_membershipHelper
				.Setup(o => o.GetUser())
				.Returns(_currentUser);

			_membershipHelper
				.Setup(o => o.CurrentUser)
				.Returns(_currentUser);
		}


		[TestMethod]
		public void IsSectionValid_ShouldReturnTrue_WhenRowBelongsToSameSection()
		{
			var controller = GetSecuredController();

			var result = controller.IsSectionValid(_objInSection, o => o.SectionId);

			Assert.IsTrue(result, "Should pass validation for objects in same section");
		}

		[TestMethod]
		public void IsSectionValid_ShouldReturnFalse_WhenRowDoesntBelongsToSameSection()
		{
			var controller = GetSecuredController();

			var result = controller.IsSectionValid(_objOutSection, o => o.SectionId);

			Assert.IsFalse(result, "Shouldn't pass validation for objects in other section");
		}

		private SecuredController GetSecuredController()
		{
			var controller = new SecuredController();
			controller.MembershipHelper = _membershipHelper.Object;
			return controller;
		}
	}
}
