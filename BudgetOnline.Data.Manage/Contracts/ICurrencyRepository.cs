using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ICurrencyRepository
	{
		IEnumerable<Currency> GetList(int sectionId);
		Currency GetDefault(int sectionId);
		Currency Insert(Currency row);
		void Update(Currency row);

		Currency Get(int id);
	}
}
