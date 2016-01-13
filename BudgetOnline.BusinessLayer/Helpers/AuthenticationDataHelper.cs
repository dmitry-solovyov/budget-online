using System;
using System.Linq;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Helpers
{
    public class AuthenticationDataHelper : IAuthenticationDataHelper
    {
        private const string CurrentOrigin = "api";

        public IUserRepository UserRepository { get; set; }
        public ISettingsHelper SettingsHelper { get; set; }
        public IUserPasswordRepository UserPasswordRepository { get; set; }
        public IUserConnectRepository UserConnectRepository { get; set; }
        public ILogWriter Log { get; set; }

        public AccountCheckResult ValidateLogin(string userName, string password)
        {
            var userPasswordInfo = CheckAccount(userName);

            if (userPasswordInfo.UserPassword != null)
            {
                if (!userPasswordInfo.UserPassword.Password.Equals(password))
                {
                    userPasswordInfo.Status = AccountCheckStatuses.PasswordNotMatch;
                    userPasswordInfo.UserPassword = null;
                }
            }

            return userPasswordInfo;
        }

        public AccountCheckResult CheckAccount(string userName)
        {
            var dbUser = UserRepository.FindByEmail(userName);

            if (dbUser != null)
                return CheckAccount(dbUser.Id);

            return new AccountCheckResult
            {
                Status = AccountCheckStatuses.UserNotFound
            };
        }

        public AccountCheckResult CheckAccount(int userId)
        {
            var result = new AccountCheckResult();

            var dbUser = UserRepository.GetUser(userId);

            result.Status = CheckLoginValidity(dbUser);
            if (result.Status != AccountCheckStatuses.Ok)
                return result;

            var dbPassword = UserPasswordRepository.GetPasswords(dbUser.Id).Last();

            result.User = dbUser;
            result.Status = CheckPasswordValidity(dbPassword, null, dbUser.SectionId);
            if (result.Status != AccountCheckStatuses.Ok)
                return result;

            result.UserPassword = dbPassword;

            return result;
        }

        private AccountCheckStatuses CheckLoginValidity(User user)
        {
            if (user == null || user.Id <= 0)
                return AccountCheckStatuses.UserNotFound;

            if (user.IsDisabled)
                return AccountCheckStatuses.UserDisabled;

            return AccountCheckStatuses.Ok;
        }

        private AccountCheckStatuses CheckPasswordValidity(UserPassword recentPassword, string password, int sectionId)
        {
            if (recentPassword == null || recentPassword.Id <= 0)
                return AccountCheckStatuses.PasswordNotFound;

            if (recentPassword.IsDisabled)
            {
                Log.DebugFormat("User's password is disabled. RecentPasswordId={0}", recentPassword.Id);
                return AccountCheckStatuses.PasswordDisabled;
            }

            var passwordValidityPeriod = SettingsHelper.PasswordValidityPeriod(sectionId);
            if (passwordValidityPeriod > TimeSpan.Zero && recentPassword.CreatedWhen <= DateTime.UtcNow.Subtract(passwordValidityPeriod))
            {
                Log.DebugFormat("User's password is expired. RecentPasswordId={0}, Period={1}", recentPassword.Id, passwordValidityPeriod);
                return AccountCheckStatuses.PasswordExpired;
            }

            if (!string.IsNullOrWhiteSpace(password))
                if (recentPassword.Password != password)
                {
                    Log.DebugFormat("User's password doesn't match password in database. RecentPasswordId={0}", recentPassword.Id);
                    return AccountCheckStatuses.PasswordNotFound;
                }

            return AccountCheckStatuses.Ok;
        }

        private AccountCheck CheckTokenValidity(UserConnect userConnect, int sectionId)
        {
            if (string.IsNullOrWhiteSpace(userConnect.Token))
                return new AccountCheck
                {
                    Status = AccountCheckStatuses.TokenNotFound,
                    ExpiredAfter = DateTime.UtcNow
                };

            if (!userConnect.UserPasswordId.HasValue)
            {
                Log.DebugFormat("Passoword not populated for token. UserConnectId={0}", userConnect.Id);
                return new AccountCheck
                {
                    Status = AccountCheckStatuses.PasswordNotFound,
                    ExpiredAfter = DateTime.UtcNow
                };
            }

            if (!userConnect.ExpiresWhen.HasValue)
            {
                Log.DebugFormat("ExpiresWhen not updated for token. UserConnectId={0}", userConnect.Id);
                return new AccountCheck
                {
                    Status = AccountCheckStatuses.TokenNotFound,
                    ExpiredAfter = DateTime.UtcNow
                };
            }

            var passwordValidityPeriod = SettingsHelper.TokenValidityPeriod(sectionId);
            if (passwordValidityPeriod > TimeSpan.Zero)
            {
                if (userConnect.ExpiresWhen.Value <= DateTime.UtcNow.Subtract(passwordValidityPeriod))
                {
                    Log.DebugFormat("Token is expired. userConnect={0}, Period={1}", userConnect.Id,
                        passwordValidityPeriod);

                    return new AccountCheck
                    {
                        Status = AccountCheckStatuses.TokenExpired,
                        ExpiredAfter = DateTime.UtcNow
                    };
                }
            }

            return 
                new AccountCheck
                {
                    Status = AccountCheckStatuses.Ok,
                    ExpiredAfter = userConnect.ExpiresWhen.Value.ToUniversalTime()
                };
        }

        public UserConnect TrackUsersLogin(string userName)
        {
            var user = UserRepository.FindByEmail(userName);
            if (user == null)
            {
                Log.DebugFormat("User is not found. UserName={0}", userName);
                return null;
            }

            return TrackUsersLogin(user.Id);
        }

        public UserConnect TrackUsersLogin(int userId)
        {
            var user = UserRepository.GetUser(userId);

            var passwordValidityPeriod = SettingsHelper.TokenValidityPeriod(user.SectionId);

            var connect = new UserConnect
            {
                UserId = userId,
                UserConnectStatusId = UserConnectStatuses.Success,
                CreatedWhen = DateTime.UtcNow,
                ExpiresWhen = DateTime.UtcNow.Add(passwordValidityPeriod)
            };

            var newConnect = UserConnectRepository.Insert(connect);

            UserConnectRepository.MarkPreviousTokensDisabled(newConnect);

            return newConnect;
        }

        public UserConnect GetUserConnect(string token)
        {
            return UserConnectRepository.FindByToken(token);
        }

        public TimeSpan GetTokenValidityPeriod(int sectionId)
        {
            return SettingsHelper.TokenValidityPeriod(sectionId);
        }

        public UserConnect TrackUsersToken(int userId, string token, int passwordId)
        {
            var user = UserRepository.GetUser(userId);

            var passwordValidityPeriod = SettingsHelper.TokenValidityPeriod(user.SectionId);

            var connect = new UserConnect
            {
                UserId = userId,
                UserConnectStatusId = UserConnectStatuses.Success,
                Token = token,
                Origin = CurrentOrigin,
                UserPasswordId = passwordId,
                ExpiresWhen = DateTime.UtcNow.Add(passwordValidityPeriod),
                CreatedWhen = DateTime.UtcNow
            };

            return UserConnectRepository.Insert(connect);
        }

        public UserConnect TrackUsersToken(string userName, string token, int passwordId)
        {
            var user = UserRepository.FindByEmail(userName);
            if (user == null)
            {
                Log.DebugFormat("User is not found. UserName={0}", userName);
                return null;
            }

            return TrackUsersToken(user.Id, token, passwordId);
        }

        public AccountCheckResult UserFromToken(string token)
        {
            var userConnect = UserConnectRepository.FindByToken(token);
            if (userConnect == null)
                return null;

            var user = UserRepository.GetUser(userConnect.UserId);

            var checkResult = CheckTokenValidity(userConnect, user.SectionId);
            if (checkResult.Status != AccountCheckStatuses.Ok)
            {
                return new AccountCheckResult
                {
                    User = user,
                    UserConnect = userConnect,
                    Status = checkResult.Status
                };
            }

            var accountCheckResult = CheckAccount(userConnect.UserId);

            return accountCheckResult;
        }

        public void UpdateConnectUsage(UserConnect userConnect)
        {
            var user = UserRepository.GetUser(userConnect.UserId);

            var passwordValidityPeriod = SettingsHelper.TokenValidityPeriod(user.SectionId);

            UserConnectRepository.UpdateTokenUsage(userConnect.Id, DateTime.UtcNow.Add(passwordValidityPeriod));
        }
    }
}
