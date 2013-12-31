using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using AutoMapper;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;
using Transaction = BudgetOnline.Data.MSSQL.Transaction;

namespace BudgetOnline.Data.Manage.Repositories
{
    public class TransactionRepository : InternalRepository<Transaction, Types.Simple.Transaction>, ITransactionRepository
    {
        public IDateTimeProvider DateTimeProvider { get; set; }
        public ICurrentUserProvider CurrentUserProvider { get; set; }
        public ITransactionLinkRepository TransactionLinkRepository { get; set; }

        private MSSQL.BudgetOnlineDBDataContext _context;

        public MSSQL.BudgetOnlineDBDataContext Context
        {
            get { return _context ?? (_context = DatabaseContext.Get()); }
            set
            {
                _context = value;
            }
        }

        public override Table<Transaction> Source
        {
            get
            {
                return Context.Transactions;
            }
        }

        public Types.Simple.Transaction GetById(int id)
        {
            return GetSingle(o => o.Id == id);
        }

        public LinkedTransactions GetLinked(int id)
        {
            var db = Context;

            var links = (from tl in db.TransactionLinks
                         where tl.ParentId == id || tl.ChildId == id
                         select tl).ToArray();

            if (links.Length == 1)
                return new LinkedTransactions
                        {
                            First = GetById(links[0].ParentId),
                            Second = GetById(links[0].ChildId)
                        };

            return new LinkedTransactions
                        {
                            First = GetById(id),
                            Second = null
                        };


        }

        protected IQueryable<TransactionWithJoins> GetBaseQuery(int sectionId, TransactionSearchOptions options, bool isGrouped)
        {
            var db = Context;

            #region Query

            var query =
                from transaction in db.Transactions

                join tLinks in db.TransactionLinks on transaction.Id equals tLinks.ParentId into transactionTLinks
                from tJoined in transactionTLinks.DefaultIfEmpty()

                join tCat in db.Categories on transaction.CategoryId equals tCat.Id into category
                from tCategory in category.DefaultIfEmpty()

                join tCurOut in db.Currencies on transaction.CurrencyId equals tCurOut.Id into currencyOut
                from tCurrencyOut in currencyOut.DefaultIfEmpty()

                join tAccOut in db.Accounts on transaction.AccountId equals tAccOut.Id into accountOut
                from tAccountOut in accountOut.DefaultIfEmpty()

                join linkedTransaction in db.Transactions on tJoined.ChildId equals linkedTransaction.Id into linkedTransactionDb
                from tLinkedTransaction in linkedTransactionDb.DefaultIfEmpty()

                join tAccIn in db.Accounts on tLinkedTransaction.AccountId equals tAccIn.Id into accountIn
                from tAccountIn in accountIn.DefaultIfEmpty()

                join tCurIn in db.Currencies on tLinkedTransaction.CurrencyId equals tCurIn.Id into currencyIn
                from tCurrencyIn in currencyIn.DefaultIfEmpty()

                where transaction.SectionId == sectionId && transaction.IsDisabled.Equals(false)
                select new TransactionWithJoins
                        {
                            SumNegative = transaction.Sum < 0,
                            transaction = transaction,
                            tLinkedTransaction = tLinkedTransaction,
                            SumLinkedNegative = tLinkedTransaction.Sum < 0,
                            tCategory = tCategory,
                            tCurrencyOut = tCurrencyOut,
                            tCurrencyIn = tCurrencyIn,
                            tAccountIn = tAccountIn,
                            tAccountOut = tAccountOut
                        };


            //if (options.Date1.HasValue && options.Date2.HasValue)
            //    query = query.Where(o => o.transaction.Date >= options.Date1.Value && o.transaction.Date < options.Date2.Value.Date.AddDays(1));
            if (options.Date1.HasValue)
                query = query.Where(o => o.transaction.Date >= options.Date1.Value.Date);
            if (options.Date2.HasValue)
                query = query.Where(o => o.transaction.Date < options.Date2.Value.Date.AddDays(1));


            if (options.SumSign > 0)
                query = query.Where(o => o.transaction.Sum > 0);
            else if (options.SumSign < 0)
                query = query.Where(o => o.transaction.Sum < 0);

            if (options.TransactionTypes != null && options.TransactionTypes.Any())
                query = query.Where(o => options.TransactionTypes.Contains(o.transaction.TransactionTypeId));

            if (options.Categories != null && options.Categories.Any())
                query = query.Where(o => o.transaction.CategoryId.HasValue && options.Categories.Contains(o.transaction.CategoryId.Value));

            if (options.Currencues != null && options.Currencues.Any())
                query = query.Where(o => options.Currencues.Contains(o.transaction.CurrencyId));

            if (options.Accounts != null && options.Accounts.Any())
                query = query.Where(o => options.Accounts.Contains(o.transaction.AccountId));


            if (options.ExcludeTags != null && options.ExcludeTags.Any())
                query = options.ExcludeTags.Aggregate(query, (current, tag) => current.Where(o => !o.transaction.Tags.Contains(tag)));

            if (!string.IsNullOrWhiteSpace(options.SearchText))
            {
                query = query.Where(o => o.transaction.Tags.Contains(options.SearchText)
                    || o.transaction.Description.Contains(options.SearchText)
                    || o.tCategory.Name.Contains(options.SearchText));
            }

            if (!string.IsNullOrWhiteSpace(options.Tag))
            {
                query = query.Where(o => o.transaction.Tags.Contains(options.Tag));
            }

            if (isGrouped)
            {
                //    query = query.Where(o =>
                //                        o.tAccountIn.IsExternal ||
                //                        o.tAccountOut.IsExternal ||
                //                        new[] { (int)TransactionTypes.Income, (int)TransactionTypes.Outcome }.Contains(o.transaction.TransactionTypeId)
                //        );
            }

            query = query.OrderByDescending(o => o.transaction.Date);

            if (!isGrouped)
            {
                query = query.Where(o => o.transaction.LinkedId == null);

                if (options.PageNumber > 1)
                    query = query.Skip(((options.PageNumber ?? 1) - 1) * (options.PageSize ?? 30));

                query = query.Take(options.PageSize ?? 30);
            }

            #endregion

            return query;
        }

        protected IQueryable<TransactionJoined> PrepareQuery(int sectionId, TransactionSearchOptions options)
        {
            var query = GetBaseQuery(sectionId, options, false);

            return query.Select(o => new TransactionJoined
                                {
                                    Id = o.transaction.Id,

                                    AccountOutId = o.transaction.AccountId,
                                    AccountOutName = o.tAccountOut.Name,
                                    AccountOutIsExternal = o.tAccountOut.IsExternal,
                                    CurrencyOutId = o.transaction.CurrencyId,
                                    CurrencyOutName = o.tCurrencyOut.Name,
                                    CurrencyOutSymbol = o.tCurrencyOut.Symbol,
                                    SumOut = o.transaction.Sum,
                                    SumOutNegative = o.transaction.Sum < 0,

                                    AccountInId = o.tLinkedTransaction.AccountId,
                                    AccountInName = o.tAccountIn.Name,
                                    AccountInIsExternal = o.tAccountIn.IsExternal,
                                    CurrencyInId = o.tLinkedTransaction.CurrencyId,
                                    CurrencyInName = o.tCurrencyIn.Name,
                                    CurrencyInSymbol = o.tCurrencyIn.Symbol,
                                    SumIn = o.tLinkedTransaction.Sum,
                                    SumInNegative = o.tLinkedTransaction.Sum < 0,

                                    Date = o.transaction.Date,
                                    Amount = o.transaction.Amount,
                                    Tags = o.transaction.Tags,
                                    Description = o.transaction.Description,
                                    IsDisabled = o.transaction.IsDisabled,
                                    CategoryId = o.transaction.CategoryId,
                                    CategoryName = o.tCategory.Name,
                                    CreatedBy = o.transaction.CreatedBy,
                                    CreatedWhen = o.transaction.CreatedWhen,
                                    UpdatedBy = o.transaction.UpdatedBy,
                                    UpdatedWhen = o.transaction.UpdatedWhen,
                                    TransactionTypeId = o.transaction.TransactionTypeId
                                });

        }

        public int GetListLength(int sectionId, TransactionSearchOptions options)
        {
            return PrepareQuery(sectionId, options).Count();
        }

        public IEnumerable<Types.Simple.Transaction> GetList(int sectionId, TransactionSearchOptions options)
        {
            var localItems = PrepareQuery(sectionId, options).ToList();

            foreach (var item in localItems)
            {
                var t = Mapper.DynamicMap<TransactionJoined, Types.Simple.Transaction>(item);

                t.AccountNameSource = item.AccountOutName;
                t.AccountNameTarget = item.AccountInName;
                t.Sum = item.SumOut;
                t.SumSource = item.SumOut;
                t.SumTarget = item.SumIn;
                t.CurrencyId = item.CurrencyOutId;
                t.CurrencyNameSource = item.CurrencyOutName;
                t.CurrencyNameTarget = item.CurrencyInName;
                t.CurrencySymbolSource = item.CurrencyOutSymbol;
                t.CurrencySymbolTarget = item.CurrencyInSymbol;
                t.CategoryId = item.CategoryId;
                t.CategoryName = item.CategoryName;
                t.TransactionTypeId = item.TransactionTypeId;

                yield return t;
            }
        }

        public IEnumerable<TransactionTotal> GetListTotals(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var query = GetBaseQuery(sectionId, options, true);

            var localItems = query
                    .GroupBy(o => new
                        {
                            o.transaction.Date.Month,
                            o.transaction.Date.Year,
                            Day = options.GroupBy.HasValue && options.GroupBy.Value == TimePeriodTypes.Daily ? o.transaction.Date.Day : 1,
                            AccountOutName = o.tAccountOut.Name,
                            AccountOutId = o.tAccountOut.Id,
                            AccountOutIsExternal = o.tAccountOut.IsExternal,
                            CurrencyOutId = o.tCurrencyOut.Id,
                            CurrencyOutName = o.tCurrencyOut.Name,
                            CurrencyOutSymbol = o.tCurrencyOut.Symbol,
                            CategoryId = o.tCategory.Id,
                            CategoryName = o.tCategory.Name,
                            SumOutNegative = o.SumNegative,
                        })
                    .Select(o => new TransactionTotal
                        {
                            Date = new DateTime(o.Key.Year, o.Key.Month, o.Key.Day),
                            AccountName = o.Key.AccountOutName,
                            AccountId = o.Key.AccountOutId,
                            AccountIsExternal = o.Key.AccountOutIsExternal,
                            CurrencyId = o.Key.CurrencyOutId,
                            CurrencyName = o.Key.CurrencyOutName,
                            CurrencySymbol = o.Key.CurrencyOutSymbol,
                            CategoryId = o.Key.CategoryId,
                            CategoryName = o.Key.CategoryName,
                            Sum = o.Sum(x => x.transaction.Sum),
                            Count = o.Count()
                        })
                    .ToList();

            return localItems;
        }

        public IEnumerable<string> GetTagsByNamePart(int sectionId, string tagPart)
        {
            return GetListInternal().Select(o => new { o.Tags, o.Date, o.SectionId }).Distinct()
                .Where(o => o.Tags.Contains(tagPart) && o.SectionId == sectionId)
                .OrderByDescending(o => o.Date)
                .Take(3)
                .Select(o => o.Tags).ToList();
        }

        public void Update(Types.Simple.Transaction row)
        {
            var id = row.Id;

            UpdateInternal(
                o => o.Id == id,
                record =>
                {
                    record.IsDisabled = row.IsDisabled;
                    record.TransactionTypeId = row.TransactionTypeId;
                    record.Date = row.Date;
                    record.Sum = row.Sum;
                    record.Formula = row.Formula;
                    record.AccountId = row.AccountId;
                    record.CurrencyId = row.CurrencyId;
                    
                    record.CategoryId = row.CategoryId;

                    record.Tags = row.Tags;
                    record.Description = row.Description;

                    record.UpdatedWhen = DateTimeProvider.Now(DateTimeKind.Utc);
                    record.UpdatedBy = CurrentUserProvider.UserId;
                });
        }

        public override Types.Simple.Transaction Insert(Types.Simple.Transaction record)
        {
            record.CreatedWhen = DateTimeProvider.Now(DateTimeKind.Utc);
            record.CreatedBy = CurrentUserProvider.UserId;

            return base.Insert(record);
        }

        public void Delete(int id)
        {
            var link = TransactionLinkRepository.GetByTransactionId(id);
            if (link != null && link.Id > 0)
            {
                var parentId = link.ParentId;
                var childId = link.ChildId;

                TransactionLinkRepository.Delete(link.Id);

                Delete(o => o.Id == parentId);
                Delete(o => o.Id == childId);
            }
            else
            {
                Delete(o => o.Id == id);
            }
        }

        protected class TransactionWithJoins
        {
            public bool SumNegative { get; set; }
            public bool? SumLinkedNegative { get; set; }
            public MSSQL.Transaction transaction { get; set; }
            public MSSQL.Transaction tLinkedTransaction { get; set; }
            public MSSQL.Category tCategory { get; set; }
            public MSSQL.Currency tCurrencyOut { get; set; }
            public MSSQL.Currency tCurrencyIn { get; set; }
            public MSSQL.Account tAccountIn { get; set; }
            public MSSQL.Account tAccountOut { get; set; }
        }
    }
}
