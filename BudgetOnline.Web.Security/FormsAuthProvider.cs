namespace BudgetOnline.Web.Security
{
	public class FormsAuthProvider : IAuthProvider
	{
		public ISessionWrapper SessionWrapper { get; set; }
		public IAuthenticationDataHelper AuthenticationDataHelper { get; set; }

		public bool Authenticate(string username, string password)
		{
			bool result = Membership.ValidateUser(username, password);
			if (result)
			{
				FormsAuthentication.SetAuthCookie(username, false);
				AuthenticationDataHelper.TrackUsersLogin(username);
			}

			return result;
		}

		public void LogoutCurrentUser()
		{
			FormsAuthentication.SignOut();
			//SessionWrapper.Remove(Constants.UserSessionKey);
			SessionWrapper.Abandon();
		}
	}
}