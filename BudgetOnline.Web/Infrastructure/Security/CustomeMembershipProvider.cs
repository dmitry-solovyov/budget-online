using System;
using System.Web.Mvc;
using System.Web.Security;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Web.Models;

namespace BudgetOnline.Web.Infrastructure.Security
{
	public class CustomeMembershipProvider : MembershipProvider
	{
		#region Dependencies

		public readonly IAuthenticationDataHelper AuthenticationHelper =
			DependencyResolver.Current.GetService<IAuthenticationDataHelper>();

		public readonly IMembershipHelper MembershipHelper =
			DependencyResolver.Current.GetService<IMembershipHelper>();

		public readonly ILogWriter Logger =
			DependencyResolver.Current.GetService<ILogWriter>();

		#endregion

		public override bool ValidateUser(string username, string password)
		{
			var result = AuthenticationHelper.CheckLoginInDatabase(username, password);
			if (result == CheckLoginInDatabaseStatus.Ok)
			{
				Logger.InfoFormat("ValidateUser check has passed. User={0}", username);
				return true;
			}

			Logger.InfoFormat("ValidateUser check has failed. User={0}, Status={1}", username, result);
			return false;
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { return MembershipPasswordFormat.Hashed; }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			UserModel model = MembershipHelper.GetUser(username);

			return new MembershipUser(
				"MyMembershipProvider",
				model.Name,
				model.Email,
				model.Email,
				string.Empty,
				string.Empty,
				(model.Id > 0),
				model.IsDisabled,
				DateTime.MinValue,
				DateTime.MinValue,
				DateTime.MinValue,
				DateTime.MinValue,
				DateTime.MinValue);
		}

		public override string GetUserNameByEmail(string email)
		{
			var user = MembershipHelper.GetUser(email);
			if (user != null)
				return user.Email;

			return null;
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}
	}
}