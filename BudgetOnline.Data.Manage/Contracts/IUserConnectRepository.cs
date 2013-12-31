using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IUserConnectRepository
	{
		UserConnect Insert(UserConnect connect);
		UserConnect Get(int id);
		IEnumerable<UserConnect> GetHistory(int userId);
		IEnumerable<UserConnectInfo> GetUsersWithConnects(int sectionId);
		IEnumerable<UserConnectInfo> GetUsersWithConnects(int sectionId, string searchBy);
	}
}
