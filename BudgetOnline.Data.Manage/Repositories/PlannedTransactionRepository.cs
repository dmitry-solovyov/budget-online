using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using PlannedTransaction = BudgetOnline.Data.MSSQL.PlannedTransaction;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class PlannedTransactionRepository : InternalRepository<PlannedTransaction, Types.Simple.PlannedTransaction>, IPlannedTransactionRepository
	{
		private MSSQL.BudgetOnlineDBDataContext _context;

		public MSSQL.BudgetOnlineDBDataContext Context
		{
			get { return _context ?? (_context = DatabaseContext.Get()); }
		}

		public override Table<PlannedTransaction> Source
		{
			get
			{
				return Context.PlannedTransactions;
			}
		}

		public Types.Simple.PlannedTransaction GetById(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		private IQueryable<TransactionWithJoins> PrepareQuery(int sectionId, PlannedTransactionSearchOptions options, bool isGrouped)
		{
			var db = Context;

			var query =
				from transaction in db.PlannedTransactions

				join tCat in db.Categories on transaction.CategoryId equals tCat.Id into category
				from tCategory in category.DefaultIfEmpty()

				join tCur in db.Currencies on transaction.CurrencyId equals tCur.Id into currency
				from tCurrency in currency.DefaultIfEmpty()

				join tAcc in db.Accounts on transaction.AccountId equals tAcc.Id into account
				from tAccount in account.DefaultIfEmpty()

				join tPTp in db.PeriodTypes on transaction.PeriodTypeId equals tPTp.Id into periodType
				from tPeriodType in periodType.DefaultIfEmpty()

				where transaction.SectionId == sectionId
				select new TransactionWithJoins
						{
							transaction = transaction,
							tAccount = tAccount,
							tCategory = tCategory,
							tCurrency = tCurrency,
							tPeriodType = tPeriodType
						};

			if (options.Date1.HasValue)
				query = query.Where(o => o.transaction.ToDate.Equals(null) || o.transaction.ToDate >= options.Date1.Value.Date);
			if (options.Date2.HasValue)
				query = query.Where(o => o.transaction.FromDate <= options.Date2.Value.Date);

			if (!string.IsNullOrWhiteSpace(options.SearchText))
			{
				query = query.Where(o => o.transaction.Tags.Contains(options.SearchText)
					|| o.transaction.Description.Contains(options.SearchText)
					|| o.transaction.Category.Name.Contains(options.SearchText));
			}

			if (!isGrouped)
			{
				if (options.PageNumber > 1)
					query = query.Skip(((options.PageNumber ?? 1) - 1) * (options.PageSize ?? 30));

				query = query.Take(options.PageSize ?? 30);
			}

			query = query.OrderBy(o => o.transaction.FromDate);

			return query;
		}

		public int GetListLength(int sectionId, PlannedTransactionSearchOptions options)
		{
			return PrepareQuery(sectionId, options, true).Count();
		}

		public IEnumerable<Types.Simple.PlannedTransaction> GetList(int sectionId, PlannedTransactionSearchOptions options)
		{
			var localItems =
				PrepareQuery(sectionId, options, true)
					   .Select(o => new
					   {
						   AccountName = o.tAccount.Name,
						   CurrencyName = o.tCurrency.Name,
						   CurrencySymbol = o.tCurrency.Symbol,
						   CategoryName = o.tCategory.Name,
						   PeriodType = o.tPeriodType.Name,
						   Transaction = o.transaction
					   }).ToList();

			foreach (var item in localItems)
			{
				var result = new Types.Simple.PlannedTransaction
								{
									Id = item.Transaction.Id,
									FromDate = item.Transaction.FromDate,
									ToDate = item.Transaction.ToDate,
									Description = item.Transaction.Description,
									IsDisabled = item.Transaction.IsDisabled,
									SectionId = item.Transaction.SectionId,
									Amount = item.Transaction.Amount,
									CreatedWhen = item.Transaction.CreatedWhen,
									CreatedBy = item.Transaction.CreatedBy,
									Sum = item.Transaction.Sum,
									Tags = item.Transaction.Tags,
									TransactionTypeId = item.Transaction.TransactionTypeId,
									PeriodTypeId = item.Transaction.PeriodTypeId,
									AccountId = item.Transaction.AccountId,
									AccountName = item.AccountName,
									CurrencyId = item.Transaction.CurrencyId,
									CurrencyName = item.CurrencyName,
									CurrencySymbol = item.CurrencySymbol,
									CategoryId = item.Transaction.CategoryId,
									CategoryName = item.CategoryName,
									PeriodTypeName = item.PeriodType,
								};

				yield return result;
			}
		}

		public void Update(Types.Simple.PlannedTransaction row)
		{
			var id = row.Id;

			UpdateInternal(
				o => o.Id == id,
				record =>
				{
					record.IsDisabled = row.IsDisabled;
					record.TransactionTypeId = row.TransactionTypeId;
					record.Sum = row.Sum;
					record.AccountId = row.AccountId;
					record.CurrencyId = row.CurrencyId;
					record.CategoryId = row.CategoryId;
					record.UpdatedWhen = DateTime.UtcNow;
					record.UpdatedBy = row.UpdatedBy;
					record.Tags = row.Tags;
					record.Description = row.Description;
					record.PeriodTypeId = row.PeriodTypeId;
					record.FromDate = row.FromDate;
					record.ToDate = row.ToDate;
				});
		}

		public void Delete(int id)
		{
			Delete(o => o.Id == id);
		}

		protected class TransactionWithJoins
		{
			public bool SumNegative { get; set; }
			public MSSQL.PlannedTransaction transaction { get; set; }
			public MSSQL.Category tCategory { get; set; }
			public MSSQL.Currency tCurrency { get; set; }
			public MSSQL.Account tAccount { get; set; }
			public MSSQL.PeriodType tPeriodType { get; set; }
		}
	}
}
