using System.Collections.Generic;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using Permission = BudgetOnline.Data.MSSQL.Permission;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class PermissionRepository : InternalRepository<Permission, Types.Simple.Permission>, IPermissionRepository
	{
		#region Overrides of InternalRepository<Permission,Permission>

		public override Table<Permission> Source
		{
			get { return DatabaseContext.Get().Permissions; }
		}

		#endregion

		#region Implementation of IPermissionRepository

		public IEnumerable<Types.Simple.Permission> GetList(int sectionId)
		{
			return GetList();
		}

		public Types.Simple.Permission Get(int id)
		{
			return base.GetSingle(o => o.Id == id);
		}

		#endregion
	}
}
