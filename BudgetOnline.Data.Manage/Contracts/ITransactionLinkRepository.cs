using System;
using System.Linq.Expressions;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ITransactionLinkRepository
	{
		TransactionLink GetByTransactionId(int transactionId);
		TransactionLink Insert(TransactionLink transaction);

		void Delete(int id);
		void DeleteLink(int parentId, int childId);
	}
}
