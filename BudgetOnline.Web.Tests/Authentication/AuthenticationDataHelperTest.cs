using System;
using System.Collections.Generic;
using BudgetOnline.BusinessLayer.Helpers;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Authentication
{
    [TestClass]
    public class AuthenticationDataHelperTest
    {
        private const int UserId = 1;
        private const string UserEmail = "user@test.com";
        private const string UserPass = "pass";
        private readonly TimeSpan ValidityPeriod = new TimeSpan(10, 0, 0, 0);

        private AuthenticationDataHelper _authenticationDataHelper;
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IUserPasswordRepository> _userPasswordRepositoryMock = new Mock<IUserPasswordRepository>();
        private readonly Mock<ISettingsHelper> _settingsHelperMock = new Mock<ISettingsHelper>();
        private readonly Mock<ILogWriter> _loggerMock = new Mock<ILogWriter>();

        private User GetUser()
        {
            var userMock = new User
                            {
                                Id = 1,
                                Name = "name",
                                Email = UserEmail
                            };
            return userMock;
        }

        private IEnumerable<UserPassword> GetUserPasswords()
        {
            var userPassword1 = new UserPassword
                                        {
                                            Id = 1,
                                            UserId = UserId,
                                            Password = "XXXX",
                                            IsDisabled = true,
                                            CreatedWhen = DateTime.Now.Subtract(ValidityPeriod).AddDays(-1)
                                        };

            var userPassword2 = new UserPassword
                                        {
                                            Id = 2,
                                            UserId = UserId,
                                            Password = UserPass,
                                            CreatedWhen = DateTime.Now.Subtract(ValidityPeriod).AddDays(1)
                                        };

            return new[] { userPassword1, userPassword2 };
        }

        private IEnumerable<UserPassword> GetUserPasswordsNotMatch()
        {
            var userPassword1 = new UserPassword
            {
                Id = 1,
                UserId = UserId,
                Password = "XXXX",
                IsDisabled = true,
                CreatedWhen = DateTime.Now.Subtract(ValidityPeriod).AddDays(-1)
            };

            var userPassword2 = new UserPassword
            {
                Id = 2,
                UserId = UserId,
                Password = UserPass + UserPass,
                CreatedWhen = DateTime.Now.Subtract(ValidityPeriod).AddDays(1)
            };

            return new[] { userPassword1, userPassword2 };
        }

        private IEnumerable<UserPassword> GetUserPasswordsOld()
        {
            var userPassword1 = new UserPassword
            {
                Id = 1,
                UserId = UserId,
                Password = UserPass,
                CreatedWhen = DateTime.Now.Subtract(ValidityPeriod).AddDays(-1)
            };

            return new[] { userPassword1 };
        }

        private IEnumerable<UserPassword> GetUserPasswordsDisabled()
        {
            var userPassword1 = new UserPassword
            {
                Id = 1,
                UserId = UserId,
                Password = UserPass,
                IsDisabled = true,
                CreatedWhen = DateTime.Now.Subtract(ValidityPeriod).AddDays(1)
            };

            return new[] { userPassword1 };
        }

        [TestInitialize]
        public void SetUp()
        {
            _userPasswordRepositoryMock
                .Setup(o => o.GetPasswords(It.Is<int>(i => i == UserId)))
                .Returns(GetUserPasswords());

            _userRepositoryMock
                .Setup(o => o.FindByEmail(It.Is<string>(i => i == UserEmail)))
                .Returns(GetUser());

            _settingsHelperMock
                .Setup(o => o.PasswordValidityPeriod(It.IsAny<int>()))
                .Returns(ValidityPeriod);

            _authenticationDataHelper =
                new AuthenticationDataHelper
                    {
                        SettingsHelper = _settingsHelperMock.Object,
                        UserPasswordRepository = _userPasswordRepositoryMock.Object,
                        UserRepository = _userRepositoryMock.Object,
                        Log = _loggerMock.Object
                    };
        }

        [TestMethod]
        public void CheckLoginInDatabase_ShouldReturnUser_WhenEverythingIsFine()
        {
            var result = _authenticationDataHelper.ValidateLogin(UserEmail, UserPass);

            Assert.AreEqual(AccountCheckStatus.Ok, result.Status);
        }

        [TestMethod]
        public void CheckLoginInDatabase_ShouldReturnPasswordIsExpired_WhenPasswordIsOld()
        {
            _userPasswordRepositoryMock
                .Setup(o => o.GetPasswords(It.Is<int>(i => i == UserId)))
                .Returns(GetUserPasswordsOld());

            var result = _authenticationDataHelper.ValidateLogin(UserEmail, UserPass);

            Assert.AreEqual(AccountCheckStatus.PasswordExpired, result.Status);
        }

        [TestMethod]
        public void CheckLoginInDatabase_ShouldReturnOkStatusForOldPassword_WhenPasswordCheckForExpireIsTurnedOff()
        {
            _userPasswordRepositoryMock
                .Setup(o => o.GetPasswords(It.Is<int>(i => i == UserId)))
                .Returns(GetUserPasswordsOld());

            _settingsHelperMock
                .Setup(o => o.PasswordValidityPeriod(It.IsAny<int>()))
                .Returns(TimeSpan.MaxValue);

            var result = _authenticationDataHelper.ValidateLogin(UserEmail, UserPass);

            Assert.AreEqual(AccountCheckStatus.Ok, result.Status);
        }

        [TestMethod]
        public void CheckLoginInDatabase_ShouldReturnPasswordIsDisabled_WhenPasswordIsDisabled()
        {
            _userPasswordRepositoryMock
                .Setup(o => o.GetPasswords(It.Is<int>(i => i == UserId)))
                .Returns(GetUserPasswordsDisabled());

            var result = _authenticationDataHelper.ValidateLogin(UserEmail, UserPass);

            Assert.AreEqual(AccountCheckStatus.PasswordDisabled, result.Status);
        }

        [TestMethod]
        public void CheckLoginInDatabase_ShouldReturnPasswordNotMatch_WhenPasswordNotEqual()
        {
            _userPasswordRepositoryMock
                .Setup(o => o.GetPasswords(It.Is<int>(i => i == UserId)))
                .Returns(GetUserPasswordsNotMatch());

            var result = _authenticationDataHelper.ValidateLogin(UserEmail, UserPass);

            Assert.AreEqual(AccountCheckStatus.PasswordNotMatch, result.Status);
        }
    }
}
