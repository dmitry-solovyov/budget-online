using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Repositories
{
    public class TransactionStatisticsRepository : TransactionRepository, ITransactionStatisticsRepository
    {
        public IEnumerable<TransactionTotal> GetStatistictsByTag(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var localItems =
                GetListTotals(sectionId, options)
                    .GroupBy(o => new
                                    {
                                        o.Date.Value.Month,
                                        o.Date.Value.Year,
                                        Day = options.GroupBy.HasValue && options.GroupBy.Value == TimePeriodTypes.Daily ? o.Date.Value.Day : 1,
                                        o.CurrencyId,
                                        o.CurrencyName,
                                        o.CurrencySymbol
                                    })
                    .Select(o => new TransactionTotal
                                    {
                                        Date = new DateTime(o.Key.Year, o.Key.Month, o.Key.Day),
                                        Sum = o.Sum(x => x.Sum),
                                        CurrencyId = o.Key.CurrencyId,
                                        CurrencyName = o.Key.CurrencyName,
                                        CurrencySymbol = o.Key.CurrencySymbol,
                                    })
                    .OrderByDescending(o => o.Date);

            return localItems.ToList();
        }

        public IEnumerable<TransactionTotal> GetStatistictsByAccount(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var localItems =
                GetListTotals(sectionId, options)
                    .GroupBy(o => new
                                    {
                                        o.Date.Value.Month,
                                        o.Date.Value.Year,
                                        Day = options.GroupBy.HasValue && options.GroupBy.Value == TimePeriodTypes.Daily ? o.Date.Value.Day : 1,
                                        o.AccountId,
                                        o.AccountName,
                                        o.CurrencyId,
                                        o.CurrencyName,
                                        o.CurrencySymbol
                                    })
                    .Select(o => new TransactionTotal
                                    {
                                        Date = new DateTime(o.Key.Year, o.Key.Month, o.Key.Day),
                                        Sum = o.Sum(x => x.Sum),
                                        AccountId = o.Key.AccountId,
                                        AccountName = o.Key.AccountName,
                                        CurrencyId = o.Key.CurrencyId,
                                        CurrencyName = o.Key.CurrencyName,
                                        CurrencySymbol = o.Key.CurrencySymbol
                                    })
                    .OrderByDescending(o => o.Date);

            return localItems.ToList();
        }

        public IEnumerable<TransactionTotal> GetStatistictsByCurrency(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var localItems =
                GetListTotals(sectionId, options)
                    .GroupBy(o => new
                    {
                        o.Date.Value.Month,
                        o.Date.Value.Year,
                        Day = options.GroupBy.HasValue && options.GroupBy.Value == TimePeriodTypes.Daily ? o.Date.Value.Day : 1,
                        o.CurrencyId,
                        o.CurrencyName,
                        o.CurrencySymbol
                    })
                    .Select(o => new TransactionTotal
                    {
                        Date = new DateTime(o.Key.Year, o.Key.Month, o.Key.Day),
                        Sum = o.Sum(x => x.Sum),
                        CurrencyId = o.Key.CurrencyId,
                        CurrencyName = o.Key.CurrencyName,
                        CurrencySymbol = o.Key.CurrencySymbol
                    })
                    .OrderByDescending(o => o.Date);

            return localItems.ToList();
        }

        public IEnumerable<TransactionTotal> GetStatistictsByCategory(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var localItems =
                GetListTotals(sectionId, options)
                    .GroupBy(o => new
                                    {
                                        o.Date,
                                        o.CategoryId,
                                        o.CategoryName,
                                        o.CurrencyId,
                                        o.CurrencyName,
                                        o.CurrencySymbol
                                    })
                    .Select(o => new TransactionTotal
                                    {
                                        Date = o.Key.Date,
                                        Sum = o.Sum(x => x.Sum),
                                        CategoryId = o.Key.CategoryId,
                                        CategoryName = o.Key.CategoryName,
                                        CurrencyId = o.Key.CurrencyId,
                                        CurrencyName = o.Key.CurrencyName,
                                        CurrencySymbol = o.Key.CurrencySymbol
                                    })
                    .OrderByDescending(o => o.Date);

            return localItems.ToList();
        }


        public IEnumerable<TransactionTotal> GetTotalsByCurrencies(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var items =
                GetListTotals(sectionId, options)
                    .Where(o => o.Sum != 0 && (o.AccountIsExternal == null || o.AccountIsExternal == false))
                    .GroupBy(o => new
                                    {
                                        o.CurrencyId,
                                        o.CurrencyName,
                                        o.CurrencySymbol
                                    })
                    .Select(o => new TransactionTotal
                                    {
                                        Date = DateTime.Today,
                                        CurrencyId = o.Key.CurrencyId,
                                        CurrencyName = o.Key.CurrencyName,
                                        CurrencySymbol = o.Key.CurrencySymbol,
                                        Sum = o.Sum(r => r.Sum),
                                        Count = o.Sum(r => r.Count),
                                    })
                    .OrderBy(o => o.CurrencyName);

            return items.ToList();
        }

        public IEnumerable<TransactionTotal> GetTotalsByCategories(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var items =
                GetListTotals(sectionId, options)
                    .Where(o => o.Sum != 0)
                    .GroupBy(o => new
                                    {
                                        o.CurrencyId,
                                        o.CurrencyName,
                                        o.CurrencySymbol,
                                        o.CategoryId,
                                        o.CategoryName
                                    })
                    .Select(o => new TransactionTotal
                                    {
                                        Date = DateTime.Today,
                                        CategoryId = o.Key.CategoryId,
                                        CategoryName = o.Key.CategoryName,
                                        CurrencyId = o.Key.CurrencyId,
                                        CurrencyName = o.Key.CurrencyName,
                                        CurrencySymbol = o.Key.CurrencySymbol,
                                        Sum = o.Sum(r => r.Sum),
                                        Count = o.Sum(r => r.Count),
                                    })
                    .OrderBy(o => o.CurrencyName);

            return items.ToList();
        }

        public IEnumerable<TransactionTotal> GetTotalsByAccounts(int sectionId, TransactionStatisticsSearchOptions options)
        {
            var items = GetListTotals(sectionId, options)
                .GroupBy(o => new
                                {
                                    o.CurrencyId,
                                    o.CurrencyName,
                                    o.CurrencySymbol,
                                    o.AccountId,
                                    o.AccountName,
                                    o.AccountIsExternal
                                })
                .Select(o => new
                                {
                                    GroupKey = o.Key,
                                    Sum = o.Sum(r => r.Sum)
                                })
                .OrderByDescending(o => o.GroupKey.AccountIsExternal)
                .ThenBy(o => o.GroupKey.AccountName)
                .ThenBy(o => o.GroupKey.CurrencyName);

            return items.Where(o => o.Sum != 0)
                .Select(item => new TransactionTotal
                                {
                                    Date = DateTime.Today,
                                    CurrencyId = item.GroupKey.CurrencyId,
                                    CurrencyName = item.GroupKey.CurrencyName,
                                    CurrencySymbol = item.GroupKey.CurrencySymbol,
                                    AccountId = item.GroupKey.AccountId,
                                    AccountName = item.GroupKey.AccountName,
                                    AccountIsExternal = item.GroupKey.AccountIsExternal,
                                    Sum = item.Sum
                                });
        }

    }
}
