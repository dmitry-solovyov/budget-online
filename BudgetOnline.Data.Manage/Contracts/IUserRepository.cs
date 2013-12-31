using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IUserRepository
	{
		User FindByEmail(string email);
		User GetUser(int userId);
		IEnumerable<User> GetUsers(int[] userIds);
		IEnumerable<User> GetUsers(int sectionId);
		void Update(User user);
		User Insert(User user);

		bool IsUserSectionAdmin(string email);
	}
}
