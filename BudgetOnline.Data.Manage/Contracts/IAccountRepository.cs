using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IAccountRepository
	{
		IEnumerable<Account> GetList(int sectionId);
		Account GetDefault(int sectionId);
		Account Insert(Account row);
		void Update(Account row);

		Account Get(int accountId);
	}
}
