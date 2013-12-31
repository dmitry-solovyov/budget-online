using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class UserPasswordRepository : InternalRepository<UserPassword, Types.Simple.UserPassword>, IUserPasswordRepository
	{
		public override Table<UserPassword> Source
		{
			get { return DatabaseContext.Get().UserPasswords; }
		}

		public Types.Simple.UserPassword GetPassword(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		public IEnumerable<Types.Simple.UserPassword> GetPasswords(int userId)
		{
			return GetListInternal().Where(o => o.UserId == userId && o.IsDisabled == false)
				.OrderBy(o => o.CreatedWhen)
				.Select(o => MappingHelper.OutMapper(o));
		}
	}
}
