using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IUserPasswordRepository
	{
		UserPassword GetPassword(int id);
		IEnumerable<UserPassword> GetPasswords(int userId);
	}
}
