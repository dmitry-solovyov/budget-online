using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ITransactionRepository
	{
		Transaction GetById(int id);
		LinkedTransactions GetLinked(int id);

		int GetListLength(int sectionId, TransactionSearchOptions options);
		IEnumerable<Transaction> GetList(int sectionId, TransactionSearchOptions options);
        IEnumerable<TransactionTotal> GetListTotals(int sectionId, TransactionStatisticsSearchOptions options);
		IEnumerable<string> GetTagsByNamePart(int sectionId, string tagPart);
		Transaction Insert(Transaction transaction);
		void Update(Transaction transaction);
		void Delete(int id);

        IEnumerable<Transaction> FindSimilar(int sectionId, Transaction transaction);
	}
}
