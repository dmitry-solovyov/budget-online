using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IPlannedTransactionRepository
	{
		PlannedTransaction GetById(int id);

		int GetListLength(int sectionId, PlannedTransactionSearchOptions options);
		IEnumerable<PlannedTransaction> GetList(int sectionId, PlannedTransactionSearchOptions options);
		PlannedTransaction Insert(PlannedTransaction transaction);
		void Update(PlannedTransaction transaction);
		void Delete(int id);
	}
}
