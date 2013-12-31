using System.Linq;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using BudgetOnline.Web.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Authentication
{
	[TestClass]
	public class CustomRoleProviderTests
	{
		private readonly Mock<IMembershipHelper> _membershipHelperMock = new Mock<IMembershipHelper>();
		private readonly Mock<ILogWriter> _logWriterMock = new Mock<ILogWriter>();

		[TestInitialize]
		public void Setup()
		{
			SetupMembershipHelperMock();
		}

		[TestMethod]
		public void GetRoleProvider_ShouldReturnTrue_WhenIsUserInRoleCalledWithExistingRole()
		{
			var provider = GetRoleProvider();

			var result = provider.IsUserInRole(TestUtils.GetRandomString(10), Roles.FactAdd.ToString());

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetRoleProvider_ShouldReturnFalse_WhenIsUserInRoleCalledWithNonExistingRole()
		{
			var provider = GetRoleProvider();

			var result = provider.IsUserInRole(TestUtils.GetRandomString(10), Roles.SystemAdmin.ToString());

			Assert.IsFalse(result);
		}


		[TestMethod]
		public void GetRolesForUser_ShouldReturnCorrectListOfRoles()
		{
			var provider = GetRoleProvider();

			var result = provider.GetRolesForUser(TestUtils.GetRandomString(10));

			Assert.AreEqual(4, result.Length);
			Assert.IsTrue(result.Contains(Roles.FactAdd.ToString()));
			Assert.IsTrue(result.Contains(Roles.FactView.ToString()));
			Assert.IsTrue(result.Contains(Roles.PlanAdd.ToString()));
			Assert.IsTrue(result.Contains(Roles.PlanView.ToString()));

			Assert.IsFalse(result.Contains(Roles.SectionAdmin.ToString()));
			Assert.IsFalse(result.Contains(Roles.SystemAdmin.ToString()));
		}


		[TestMethod]
		public void GetRolesForUser_ShouldReturnEmptyList_WhenUserIsDisabled()
		{
			var provider = GetRoleProvider();

			var result = provider.GetRolesForUser(string.Empty);

			Assert.AreEqual(0, result.Length);
		}

		private CustomRoleProvider GetRoleProvider()
		{
			return new CustomRoleProvider
					{
						MembershipHelper = _membershipHelperMock.Object,
						Log = _logWriterMock.Object
					};
		}

		private UserModel GetUserModel(string email, bool disabled = false)
		{
			return new UserModel
					{
						Id = 1,
						Email = email,
						IsDisabled = disabled,
						Permissions = new[]
						              	{
											new UserPermission { UserId = 1, PermissionId = Roles.FactAdd.GetRealId() },
											new UserPermission { UserId = 1, PermissionId = Roles.FactView.GetRealId() },
											new UserPermission { UserId = 1, PermissionId = Roles.PlanAdd.GetRealId() },
											new UserPermission { UserId = 1, PermissionId = Roles.PlanView.GetRealId() }
						              	}
					};
		}

		private void SetupMembershipHelperMock()
		{
			_membershipHelperMock
				.Setup(o => o.GetUser(It.Is<string>(s => s.Length > 0)))
				.Returns<string>(s => GetUserModel(s));

			_membershipHelperMock
				.Setup(o => o.GetUser(It.Is<string>(s => s.Length == 0)))
				.Returns<string>(s => GetUserModel(s, true));
		}
	}
}
