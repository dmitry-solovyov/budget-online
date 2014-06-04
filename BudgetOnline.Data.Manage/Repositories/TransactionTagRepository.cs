using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using TransactionTag = BudgetOnline.Data.MSSQL.TransactionTag;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class TransactionTagRepository : InternalRepository<TransactionTag, Types.Simple.TransactionTag>, ITransactionTagRepository
	{
		#region Overrides of InternalRepository<TransactionTag,TransactionTag>

		public override Table<TransactionTag> Source
		{
			get { return DatabaseContext.Get().TransactionTags; }
		}

		#endregion

		#region Implementation of ITransactionTagRepository

		public IEnumerable<Types.Simple.TransactionTag> GetList(int sectionId)
		{
			return GetMappedItems(
				GetListInternal().Where(o => o.SectionId == sectionId).OrderBy(o => o.Tag));
		}

		public IEnumerable<Types.Simple.TransactionTag> GetByTransaction(int transactionId)
		{
			return GetMappedItems(
				GetListInternal().Where(o => o.TransactionId == transactionId).OrderBy(o => o.CreatedWhen));
		}

		public IEnumerable<string> GetByNamePart(int sectionId, string namePart)
		{
			return 
				GetListInternal()
				    .Where(o => o.SectionId == sectionId && o.Tag.Contains(namePart))
				    .OrderByDescending(o => o.CreatedWhen)
				    .Select(o => o.Tag)
                    .Distinct()
                    .Take(5);
		}

		public void Update(Types.Simple.TransactionTag row)
		{
			UpdateInternal(
				o => o.Id == row.Id,
				record =>
				{
					record.IsDisabled = row.IsDisabled;
					record.UpdatedWhen = DateTime.UtcNow;
					record.UpdatedBy = row.UpdatedBy;
					record.Tag = row.Tag;
					record.TagId = row.TagId;
				});
		}

		public void Delete(Types.Simple.TransactionTag row)
		{
			base.Delete(o => o.Id == row.Id);
		}

		public Types.Simple.TransactionTag Get(int id)
		{
			return base.GetSingle(o => o.Id == id);
		}

		#endregion
	}
}
