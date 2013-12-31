using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using UserPermission = BudgetOnline.Data.MSSQL.UserPermission;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class UserPermissionRepository : InternalRepository<UserPermission, Types.Simple.UserPermission>, IUserPermissionRepository
	{
		#region Overrides of InternalRepository<UserPermission,UserPermission>

		public override Table<UserPermission> Source
		{
			get { return DatabaseContext.Get().UserPermissions; }
		}

		#endregion

		#region Implementation of IUserPermissionRepository

		public void Delete(int id)
		{
			Delete(o => o.Id == id);
		}

		public IEnumerable<Types.Simple.UserPermission> GetPermissions(int userId)
		{
			return GetList(o => o.UserId == userId);
		}

		public IEnumerable<Types.Simple.UserPermission> GetPermissions(string email)
		{
			var db = DatabaseContext.Get();
			var user = db.Users.FirstOrDefault(o => o.Email.Equals(email));
			if (user == null)
				return Enumerable.Empty<Types.Simple.UserPermission>();

			return GetList(o => o.UserId == user.Id);
		}

		public Types.Simple.UserPermission Get(int id)
		{
			return base.GetSingle(o => o.Id == id);
		}

		#endregion
	}
}
