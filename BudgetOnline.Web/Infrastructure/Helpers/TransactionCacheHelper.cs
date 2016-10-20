using System;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
    public class TransactionCacheHelper : ITransactionCacheHelper
    {
        public ISessionWrapper SessionWrapper { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }

        public void BalanceUpdated()
        {
            if (SessionWrapper.IsAvailable)
                SessionWrapper.Put(BalanceChangeTimeKey, DateTimeProvider.Now());
        }

        public DateTime GetLastBalanceUpdated()
        {
            if (SessionWrapper.IsAvailable)
            {
                DateTime lastDt;
                if (SessionWrapper.Get(BalanceChangeTimeKey, out lastDt))
                    return lastDt;
            }

            return DateTime.MinValue;
        }

        public const string BalanceChangeTimeKey = "BalanceChangeTime";
    }
}