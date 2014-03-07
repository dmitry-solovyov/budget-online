using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Complex;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IUserPasswordRepository
	{
		UserPassword Get(int id);
		IEnumerable<UserPassword> GetPasswords(int userId);
	}
}
