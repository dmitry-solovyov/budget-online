using BudgetOnline.Common;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IAuthenticationDataHelper
	{
		CheckLoginInDatabaseStatus CheckLoginInDatabase(string userName, string password);
		void TrackUsersLogin(string userName);
		void TrackUsersLogin(int userId);
	}
}
