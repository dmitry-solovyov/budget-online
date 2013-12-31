using System.Collections.Generic;
using BudgetOnline.Data.Contracts.Entities;

namespace BudgetOnline.Data.Contracts.Repositories
{
	public interface IUserRepository
	{
		IUser FindByEmail(string email);
		IUser GetUser(int userId);
		IEnumerable<IUser> GetUsers(int sectionId);
	}
}
