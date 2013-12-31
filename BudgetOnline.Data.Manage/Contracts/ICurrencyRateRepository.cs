using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ICurrencyRateRepository
	{
		IEnumerable<CurrencyRate> GetList(int sectionId);
		IEnumerable<CurrencyRate> GetLastRates(int sectionId);

		CurrencyRate Insert(CurrencyRate row);
		void Update(CurrencyRate row);

		CurrencyRate Get(int id);
	}
}
