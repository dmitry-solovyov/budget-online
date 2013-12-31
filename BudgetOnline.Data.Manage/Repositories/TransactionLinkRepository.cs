using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class TransactionLinkRepository : InternalRepository<TransactionLink, Types.Simple.TransactionLink>, ITransactionLinkRepository
	{
		public override Table<TransactionLink> Source
		{
			get { return DatabaseContext.Get().TransactionLinks; }
		}

		public Types.Simple.TransactionLink GetById(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		public Types.Simple.TransactionLink GetByTransactionId(int transactionId)
		{
			return GetSingle(o => o.ParentId == transactionId || o.ChildId == transactionId);
		}


		public void Delete(int id)
		{
			Delete(o => o.Id == id);
		}

		public void DeleteLink(int parentId, int childId)
		{
			Delete(o => o.ChildId == childId || o.ParentId == parentId);
		}
	}
}
