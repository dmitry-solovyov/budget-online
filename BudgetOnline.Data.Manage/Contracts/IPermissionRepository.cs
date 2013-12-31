using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IPermissionRepository
	{
		IEnumerable<Permission> GetList(int sectionId);
		Permission Get(int id);
	}
}
