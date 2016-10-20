using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Web.Models;

namespace BudgetOnline.Web.Infrastructure.Security
{
	public class MembershipHelper : IMembershipHelper
	{
		private IPrincipal _principal;
		public IPrincipal Principal
		{
			get { return _principal ?? (_principal = HttpContext.Current != null ? HttpContext.Current.User : null); }
			set { _principal = value; }
		}

		public ISessionWrapper SessionWrapper { get; set; }
		public IUserRepository UserRepository { get; set; }
		public IAuthProvider AuthProvider { get; set; }
		public ILogWriter LogWriter { get; set; }

		public IUserPermissionRepository UserPermissionRepository { get; set; }
		public IPermissionRepository PermissionRepository { get; set; }

		public virtual UserModel CurrentUser
		{
			get { return GetUser(); }
		}

		public virtual UserModel GetUser()
		{
			if (!Principal.Identity.IsAuthenticated)
				return new UserModel();

			if (!IsSessionUserValid())
			{
				LogWriter.DebugFormat("Session contains invalid user Email={0}", Principal.Identity.Name);

				ClearUserInSession();
				return new UserModel();
			}

			var userModel = GetUser(Principal.Identity.Name);

			WriteUserFromSession(userModel);

			return userModel;
		}

		public virtual UserModel GetUser(string userEmail)
		{
			if (Principal.Identity.IsAuthenticated && Principal.Identity.Name.Equals(userEmail, StringComparison.InvariantCultureIgnoreCase))
			{
				if (IsSessionUserValid())
				{
					var user = ReadUserFromSession();
					if (user != null)
					{
						LogWriter.TraceFormat("Session object has been used for GetUser method. UserEmail={0}", userEmail);
						return user;
					}
				}
			}

			return ReadUserFromDb(userEmail);
		}

		public CultureInfo GetCulture()
		{
			return CultureInfo.GetCultureInfo("ru-RU");
		}

		public CultureInfo GetCulture(int userId)
		{
			return CultureInfo.GetCultureInfo("ru-RU");
		}

		public void SetCulture(CultureInfo culture)
		{
		}

		public virtual UserModel GetUser(int id)
		{
			var user = UserRepository.GetUser(id);
			if (user == null)
			{
				LogWriter.DebugFormat("User is not found in DB. Id={0}", id);

				return new UserModel();
			}

			return ReadUserFromDb(user.Email);
		}

		public virtual bool UsersInOneSection(params int?[] ids)
		{
			if (ids == null || ids.Length == 0)
				return false;

			var validIds = ids.Where(o => o.HasValue).Select(o => o.Value).ToArray();

			var users = UserRepository.GetUsers(validIds).ToList();
			if (!users.Any())
				return false;

			return users.GroupBy(o => o.SectionId).Count() == 1;
		}

		private UserModel ReadUserFromDb(string email)
		{
			var user = UserRepository.FindByEmail(email);
			if (user == null)
			{
				LogWriter.DebugFormat("User is not found in DB. Email={0}", email);

				return new UserModel();
			}

			var userPermissions = UserPermissionRepository.GetPermissions(user.Id);

			return new UserModel
				{
					Id = user.Id,
					Email = user.Email,
					Name = user.Name,
					SectionId = user.SectionId,
					IsDisabled = user.IsDisabled,
					Permissions = userPermissions
				};
		}

		private UserModel ReadUserFromSession()
		{
			var userModel = SessionWrapper.Get<UserModel>(Constants.UserSessionKey);
			//SessionWrapper.Put(Constants.UserSessionKey, userModel);

			return userModel;
		}

		private void WriteUserFromSession(UserModel model)
		{
			SessionWrapper.Put(Constants.UserSessionKey, model);
		}

		private bool IsSessionUserValid()
		{
			var username = Principal.Identity.Name;
			var userModel = SessionWrapper.Get<UserModel>(Constants.UserSessionKey);
			if (userModel == null)
				return true;

			var result = username.Equals(userModel.Email, StringComparison.InvariantCultureIgnoreCase);
			if (!result)
				LogWriter.DebugFormat("Session contains invalid user Email={0} SessionEmail={1}", Principal.Identity.Name, userModel.Email);

			return result;
		}

		private void ClearUserInSession()
		{
			SessionWrapper.Remove(Constants.UserSessionKey);
			SessionWrapper.Remove(Constants.UserSecurityInfoSessionKey);
			AuthProvider.LogoutCurrentUser();
		}
	}
}