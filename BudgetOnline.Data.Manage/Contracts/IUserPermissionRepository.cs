using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IUserPermissionRepository
	{
		UserPermission Get(int id);
		IEnumerable<UserPermission> GetPermissions(int userId);

		UserPermission Insert(UserPermission userPermission);
		void Delete(int id);
	}
}
