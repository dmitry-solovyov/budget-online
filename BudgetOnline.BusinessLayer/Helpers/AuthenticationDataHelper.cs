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
                    userPasswordInfo.Status = AccountCheckStatus.PasswordNotMatch;
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
                Status = AccountCheckStatus.UserNotFound
            };
        }

        public AccountCheckResult CheckAccount(int userId)
        {
            var result = new AccountCheckResult();

            var dbUser = UserRepository.GetUser(userId);

            result.Status = CheckLoginValidity(dbUser);
            if (result.Status != AccountCheckStatus.Ok)
                return result;

            var dbPassword = UserPasswordRepository.GetPasswords(dbUser.Id).Last();

            result.User = dbUser;
            result.Status = CheckPasswordValidity(dbPassword, null, dbUser.SectionId);
            if (result.Status != AccountCheckStatus.Ok)
                return result;

            result.UserPassword = dbPassword;

            return result;
        }

        private AccountCheckStatus CheckLoginValidity(User user)
        {
            if (user == null || user.Id <= 0)
                return AccountCheckStatus.UserNotFound;

            if (user.IsDisabled)
                return AccountCheckStatus.UserDisabled;

            return AccountCheckStatus.Ok;
        }

        private AccountCheckStatus CheckPasswordValidity(UserPassword recentPassword, string password, int sectionId)
        {
            if (recentPassword == null || recentPassword.Id <= 0)
                return AccountCheckStatus.PasswordNotFound;

            if (recentPassword.IsDisabled)
            {
                Log.DebugFormat("User's password is disabled. RecentPasswordId={0}", recentPassword.Id);
                return AccountCheckStatus.PasswordDisabled;
            }

            var passwordValidityPeriod = SettingsHelper.PasswordValidityPeriod(sectionId);
            if (passwordValidityPeriod > TimeSpan.Zero && recentPassword.CreatedWhen <= DateTime.UtcNow.Subtract(passwordValidityPeriod))
            {
                Log.DebugFormat("User's password is expired. RecentPasswordId={0}, Period={1}", recentPassword.Id, passwordValidityPeriod);
                return AccountCheckStatus.PasswordExpired;
            }

            if (!string.IsNullOrWhiteSpace(password))
                if (recentPassword.Password != password)
                {
                    Log.DebugFormat("User's password doesn't match password in database. RecentPasswordId={0}", recentPassword.Id);
                    return AccountCheckStatus.PasswordNotFound;
                }

            return AccountCheckStatus.Ok;
        }

        private AccountCheckStatus CheckTokenValidity(UserConnect userConnect, int sectionId)
        {
            if (string.IsNullOrWhiteSpace(userConnect.Token))
                return AccountCheckStatus.TokenNotFound;

            if (!userConnect.UserPasswordId.HasValue)
            {
                Log.DebugFormat("Passoword not populated for token. UserConnectId={0}", userConnect.Id);
                return AccountCheckStatus.PasswordNotFound;
            }

            if (!userConnect.LastUsed.HasValue)
            {
                Log.DebugFormat("LastUsed not updated for token. UserConnectId={0}", userConnect.Id);
                return AccountCheckStatus.TokenNotFound;
            }

            var passwordValidityPeriod = SettingsHelper.TokenValidityPeriod(sectionId);
            if (passwordValidityPeriod > TimeSpan.Zero)
            {
                if (userConnect.LastUsed.Value <= DateTime.UtcNow.Subtract(passwordValidityPeriod))
                {
                    Log.DebugFormat("Token is expired. userConnect={0}, Period={1}", userConnect.Id, passwordValidityPeriod);
                    return AccountCheckStatus.TokenExpired;
                }
            }

            return AccountCheckStatus.Ok;
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
            var connect = new UserConnect
            {
                UserId = userId,
                UserConnectStatusId = UserConnectStatuses.Success,
                CreatedWhen = DateTime.UtcNow,
            };

            return UserConnectRepository.Insert(connect);
        }

        public UserConnect TrackUsersToken(int userId, string token, int passwordId)
        {
            var connect = new UserConnect
            {
                UserId = userId,
                UserConnectStatusId = UserConnectStatuses.Success,
                Token = token,
                Origin = "api",
                UserPasswordId = passwordId,
                LastUsed = DateTime.UtcNow,
                CreatedWhen = DateTime.UtcNow,
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

            var result = CheckAccount(userConnect.UserId);
            if (result.Status == AccountCheckStatus.Ok)
            {
                result.Status = CheckTokenValidity(userConnect, result.User.SectionId);

                if (result.Status == AccountCheckStatus.Ok)
                    result.UserConnect = userConnect;
            }

            return result;
        }


    }
}
