using System.Web.Mvc;
using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.ViewModels.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Authentication
{
	[TestClass]
	public class AuthProviderTests
	{
		private Mock<IAuthProvider> _authProviderMock;

		[TestInitialize]
		public void TestSetup()
		{
			_authProviderMock = new Mock<IAuthProvider>();
			_authProviderMock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
		}

		[TestMethod]
		public void CanLoginWithValidCredentials()
		{
			var model = new LogInViewModel
			{
				UserName = "admin",
				Password = "secret"
			};

			var target = new LogInController();
			target.AuthProvier = _authProviderMock.Object;

			ActionResult result = target.Index(model);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			Assert.AreEqual("", ((RedirectToRouteResult)result).RouteName);
		}

		[TestMethod]
		public void CannotLoginWithInvalidCredentials()
		{
			var model = new LogInViewModel
			{
				UserName = "badUser",
				Password = "badPass"
			};

			var controller = new LogInController();
			controller.AuthProvier = _authProviderMock.Object;

			ActionResult result = controller.Index(model);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
			Assert.AreNotEqual(0, ((ViewResult)result).ViewData.ModelState.Count);
			//ModelState.AddModelError("login_pass_incorrect", "User/password incorrect");
		} 


	}
}
