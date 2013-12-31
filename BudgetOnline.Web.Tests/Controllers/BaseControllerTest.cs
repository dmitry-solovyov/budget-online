using BudgetOnline.Common.Contracts;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Controllers
{
	[TestClass]
	public abstract class BaseControllerTest
	{
		protected Mock<MembershipHelper> MembershipHelperMock = new Mock<MembershipHelper>();
		protected Mock<ICacheWrapper> CacheWrapperMock = new Mock<ICacheWrapper>();

		protected UserModel CurrentUser;

		[TestInitialize]
		public void TestSetup()
		{
			SetupCurrentUser();
			SetupMembershipHelperMock();
			SetupCacheWrapperMock();

			Setup();
		}

		protected abstract void Setup();

		protected void SetupCurrentUser()
		{
			CurrentUser = new UserModel
			{
				Id = 1,
				SectionId = 1,
			};
		}

		protected void SetupMembershipHelperMock()
		{
			MembershipHelperMock = new Mock<MembershipHelper>();
			MembershipHelperMock
				.Setup(o => o.GetUser())
				.Returns(CurrentUser);

			MembershipHelperMock
				.SetupGet(o => o.CurrentUser)
				.Returns(CurrentUser);
		}

		protected void SetupCacheWrapperMock()
		{
			CacheWrapperMock = new Mock<ICacheWrapper>();
			CacheWrapperMock.Setup(o => o.Exists(It.IsAny<string>())).Returns(false);
		}
	}
}
