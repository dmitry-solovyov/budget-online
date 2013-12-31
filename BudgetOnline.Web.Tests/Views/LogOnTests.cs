using System.Web.Mvc;
using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.ViewModels.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Views
{
	[TestClass]
	public class LogOnTests
	{
		private const string VALID_USER_NAME = "valid";
		private const string VALID_USER_PASS = "pass";
		private readonly Mock<IAuthProvider> _authProviderMock = new Mock<IAuthProvider>();

		[TestInitialize]
		public void SetUp()
		{
			_authProviderMock
				.Setup(m => m.Authenticate(It.Is<string>(user => user != VALID_USER_NAME), It.IsAny<string>()))
				.Returns(false);
			_authProviderMock
				.Setup(m => m.Authenticate(It.Is<string>(user => user == VALID_USER_NAME), It.Is<string>(pass => pass.Equals(VALID_USER_PASS))))
				.Returns(true);
		}

		[TestMethod]
		public void LogOnShouldRedirect_WhenValidUser()
		{
			var controller = new LogInController();
			controller.AuthProvier = _authProviderMock.Object;

			var result = controller.Index(new LogInViewModel { UserName = VALID_USER_NAME, Password = VALID_USER_PASS });

			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void LogOnShouldReturnPageWithError_WhenInvalidUser()
		{
			var controller = new LogInController();
			controller.AuthProvier = _authProviderMock.Object;

			var result = controller.Index(new LogInViewModel { UserName = "bad_login", Password = "pass" });

			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(LogInViewModel));
			Assert.AreEqual(string.Empty, ((ViewResult)result).ViewName);
		}

		[TestMethod]
		public void LogOnShouldPutErrorIntoModel_WhenInvalidUser()
		{
			var controller = new LogInController();
			controller.AuthProvier = _authProviderMock.Object;

			var result = (ViewResult)controller.Index(new LogInViewModel { UserName = "bad_login", Password = "pass" });

			Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
		}

		//[TestMethod]
		//public void LogOnShouldRenderPage_WhenValidUser()
		//{
		//    var controller = new LogInController(_authProvider.Object);
		//    var result = (ViewResult)controller.Index(new LogInViewModel { UserName = "bad_login", Password = "pass" });

		//    var viewContext = new ViewContext();
		//    var st = new StringWriter();
			
		//    result.ExecuteResult(controller.ControllerContext);

		//    result.View.Render(viewContext, st);

		//    Assert.IsNotNull(st.GetStringBuilder().ToString());
		//}
	}
}
