using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class UserPasswordRepository : InternalRepository<UserPassword, Types.Complex.UserPassword>, IUserPasswordRepository
	{
		public override Table<UserPassword> Source
		{
			get { return DatabaseContext.Get().UserPasswords; }
		}

		public Types.Complex.UserPassword Get(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		public IEnumerable<Types.Complex.UserPassword> GetPasswords(int userId)
		{
			return GetListInternal().Where(o => o.UserId == userId && o.IsDisabled == false)
				.OrderBy(o => o.CreatedWhen)
				.Select(o => MappingHelper.OutMapper(o));
		}
	}
}
