using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ITransactionSearchHelper
	{
		IEnumerable<Transaction> Search(int sectionId, string search);
		IEnumerable<Transaction> Search(int sectionId, string search, int? page, int? pageSize);
		IEnumerable<TransactionTotal> SearchTotals(int sectionId, string search);
	}
}
