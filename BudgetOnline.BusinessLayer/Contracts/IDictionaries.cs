using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Contracts
{
	public interface IDictionaries
	{
		IEnumerable<Account> Accounts();
		void ResetAccounts();
		IEnumerable<Currency> Currencies();
		void ResetCurrencies();
		IEnumerable<Category> Categories();
		void ResetCategories();
		IEnumerable<PeriodType> PeriodTypes();
		void ResetPeriodTypes();
	}
}