namespace BudgetOnline.Web.Infrastructure.Security
{
	public interface IAuthProvider
	{
		bool Authenticate(string username, string password);
		void LogoutCurrentUser();
	} 
}