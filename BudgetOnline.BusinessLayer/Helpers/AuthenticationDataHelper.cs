using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
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

		public CheckLoginInDatabaseStatus CheckLoginInDatabase(string userName, string password)
		{
			var user = UserRepository.FindByEmail(userName);
			if (user == null)
			{
				Log.DebugFormat("User is not found. UserName={0}", userName);
				return CheckLoginInDatabaseStatus.UserNotFound;
			}

			if (user.IsDisabled)
			{
				Log.DebugFormat("User is disabled. UserId={0}", user.Id);
				return CheckLoginInDatabaseStatus.UserDisabled;
			}

			var passwords = UserPasswordRepository.GetPasswords(user.Id);

			return CheckPasswordValidity(passwords, password, user.SectionId);
		}

		private CheckLoginInDatabaseStatus CheckPasswordValidity(IEnumerable<UserPassword> passwords, string password, int sectionId)
		{
			if (passwords == null || !passwords.Any()) return CheckLoginInDatabaseStatus.PasswordNotFound;

			var recentPassword = passwords.Last();
			if (recentPassword.IsDisabled)
			{
				Log.DebugFormat("User's password is disabled. RecentPasswordId={0}", recentPassword.Id);
				return CheckLoginInDatabaseStatus.PasswordIsDisabled;
			}

			int passwordValidityPeriod = SettingsHelper.PasswordValidityPeriod(sectionId);
			if (passwordValidityPeriod > 0 && recentPassword.CreatedWhen <= DateTime.UtcNow.Date.AddDays(-passwordValidityPeriod))
			{
				Log.DebugFormat("User's password is expired. RecentPasswordId={0}, Period={1}", recentPassword.Id, passwordValidityPeriod);
				return CheckLoginInDatabaseStatus.PasswordIsExpired;
			}

			if (recentPassword.Password != password)
			{
				Log.DebugFormat("User's password doesn't match password in database. RecentPasswordId={0}", recentPassword.Id);
				return CheckLoginInDatabaseStatus.PasswordNotMatch;
			}

			return CheckLoginInDatabaseStatus.Ok;
		}

		public void TrackUsersLogin(string userName)
		{
			var user = UserRepository.FindByEmail(userName);
			if (user == null)
			{
				Log.DebugFormat("User is not found. UserName={0}", userName);
				return;
			}

			TrackUsersLogin(user.Id);
		}

		public void TrackUsersLogin(int userId)
		{
			var connect = new UserConnect
			{
				UserId = userId,
				UserConnectStatusId = UserConnectStatuses.Success,
				CreatedWhen = DateTime.UtcNow
			};

			UserConnectRepository.Insert(connect);
		}
	}
}
