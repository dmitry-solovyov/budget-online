using System.Collections.Generic;
using BudgetOnline.Data.Contracts.Entities;

namespace BudgetOnline.Data.Contracts.Repositories
{
	public interface IUserPasswordRepository
	{
		IUserPassword GetPassword(int id);
		IEnumerable<IUserPassword> GetPasswords(int userId);
	}
}
