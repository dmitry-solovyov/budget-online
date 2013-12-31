using System.Security.Principal;
using BudgetOnline.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Areas.Admin.Models;
using BudgetOnline.Web.Infrastructure;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Authentication
{
	[TestClass]
	public class MembershipHelperTests
	{
		private readonly Mock<IAuthProvider> _authProviderMock = new Mock<IAuthProvider>();
		private readonly Mock<ISessionWrapper> _sessionWrapperMock = new Mock<ISessionWrapper>();
		private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
		private readonly Mock<IUserPermissionRepository> _userPermissionRepositoryMock = new Mock<IUserPermissionRepository>();
		private readonly Mock<IPrincipal> _principalMock = new Mock<IPrincipal>();
		private readonly Mock<IIdentity> _identityMock = new Mock<IIdentity>();

		private readonly User _userFromDb1 = new User { Id = 1, Email = Email, SectionId = 1 };
		private readonly User _userFromDb2 = new User { Id = 2, Email = Email, SectionId = 1 };
		private readonly User _userFromDb3OtherSection = new User { Id = 567, Email = Email, SectionId = 999 };
		private UserEditViewModel _user;

		private const string Email = @"aaa@aaa.com";
		private const string EmailIncorrect = @"bbb@aaa.com";

		[TestInitialize]
		public void SetUp()
		{
			_user = new UserEditViewModel
			{
				Email = Email,
				Id = _userFromDb1.Id,
				SectionId = 1
			};


			_sessionWrapperMock
				.Setup(o => o.Get<UserEditViewModel>(It.Is<string>(s => s == Constants.UserSessionKey)))
				.Returns(_user);

			_principalMock
				.Setup(ctx => ctx.Identity)
				.Returns(_identityMock.Object);

			_userRepositoryMock
				.Setup(o => o.FindByEmail(It.Is<string>(s => s == Email)))
				.Returns(_userFromDb1);

			_userRepositoryMock
				.Setup(o => o.GetUsers(It.Is<int[]>(p => p.Length == 2 && p[0] == _userFromDb1.Id && p[1] == _userFromDb2.Id)))
				.Returns(new[] { _userFromDb1, _userFromDb2 });

			_userRepositoryMock
				.Setup(o => o.GetUsers(It.Is<int[]>(p => p.Length == 2 && p[0] == _userFromDb1.Id && p[1] == _userFromDb3OtherSection.Id)))
				.Returns(new[] { _userFromDb1, _userFromDb3OtherSection });

			_identityMock.Setup(id => id.IsAuthenticated).Returns(true);
			_identityMock.Setup(id => id.Name).Returns(Email);

			SetupUserPermissionRepository();
		}

		[TestMethod]
		public void GetUser_ShouldReturnCurrentUser_WhenSessionContainsCorrectUser()
		{
			var result = GetMembershipHelper().GetUser();

			Assert.IsNotNull(result);
			Assert.AreEqual(Email, result.Email);
		}

		[TestMethod]
		public void GetUser_ShouldReturnCurrentUserFromDb_WhenSessionDoesntContainsUser()
		{
			_sessionWrapperMock
				.Setup(o => o.Get<UserEditViewModel>(It.Is<string>(s => s == Constants.UserSessionKey)))
				.Returns((UserEditViewModel)null);

			var result = GetMembershipHelper().GetUser();

			Assert.IsNotNull(result);
			Assert.AreEqual(Email, result.Email);
		}

		[TestMethod]
		public void GetUser_ShouldReturnEmptyUser_WhenSessionUserDiffersFromPrincipal()
		{
			_identityMock.Setup(id => id.IsAuthenticated).Returns(true);
			_identityMock.Setup(id => id.Name).Returns(EmailIncorrect);

			var result = GetMembershipHelper().GetUser();

			Assert.IsNotNull(result);
			Assert.IsTrue(string.IsNullOrWhiteSpace(result.Email));
		}


		[TestMethod]
		public void GetUser_ShouldLogOutCurrentUser_WhenSessionUserDiffersFromPrincipal()
		{
			_identityMock.Setup(id => id.IsAuthenticated).Returns(true);
			_identityMock.Setup(id => id.Name).Returns(EmailIncorrect);

			_sessionWrapperMock
				.Setup(o => o.Get<UserModel>(It.Is<string>(s => s == Constants.UserSessionKey)))
				.Returns(new UserModel { Id = -1, Email = string.Empty });

			GetMembershipHelper().GetUser();

			_authProviderMock.Verify(o => o.LogoutCurrentUser(), Times.Once(), "Should log out current user");
		}

		[TestMethod]
		public void GetUser_ShouldReturnRequestedUser_WhenUsedValidUserName()
		{
			var result = GetMembershipHelper().GetUser(Email);

			Assert.IsNotNull(result);
			Assert.AreEqual(Email, result.Email);
		}

		[TestMethod]
		public void GetUser_ShouldReturnEmptyUser_WhenUsedInvalidUserName()
		{
			var result = GetMembershipHelper().GetUser(EmailIncorrect);

			Assert.IsNotNull(result);
			Assert.IsTrue(string.IsNullOrWhiteSpace(result.Email));
		}

		[TestMethod]
		public void UsersInOneSection_ShouldReturnTrue_WhenUsedsInOneSection()
		{
			var result = GetMembershipHelper().UsersInOneSection(_userFromDb1.Id, _userFromDb2.Id);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UsersInOneSection_ShouldReturnFalse_WhenUsedsInDifferentSection()
		{
			var result = GetMembershipHelper().UsersInOneSection(_userFromDb1.Id, _userFromDb3OtherSection.Id);

			Assert.IsFalse(result);
		}

		private MembershipHelper GetMembershipHelper()
		{
			var result = new MembershipHelper
							{
								AuthProvider = _authProviderMock.Object,
								SessionWrapper = _sessionWrapperMock.Object,
								UserRepository = _userRepositoryMock.Object,
								Principal = _principalMock.Object,
								UserPermissionRepository = _userPermissionRepositoryMock.Object
							};

			return result;
		}

		private void SetupUserPermissionRepository()
		{
			_userPermissionRepositoryMock
				.Setup(o => o.GetPermissions(It.IsAny<int>()))
				.Returns<int>(userId => new[]
				         	{
				         		new UserPermission{ Id = 1, UserId = userId, PermissionId = (int)Roles.FactAdd}, 
								new UserPermission{ Id = 1, UserId = userId, PermissionId = (int)Roles.FactView}, 
				         	});

		}
	}
}
