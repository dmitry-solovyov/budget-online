using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
    public interface ITransactionTagRepository
	{
		IEnumerable<TransactionTag> GetList(int sectionId);
		IEnumerable<TransactionTag> GetByTransaction(int transactionId);
		IEnumerable<string> GetByNamePart(int sectionId, string namePart);

		TransactionTag Insert(TransactionTag row);
		void Update(TransactionTag row);
		void Delete(TransactionTag row);

		TransactionTag Get(int id);
	}
}
