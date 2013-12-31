using System;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
    public interface ITransactionCacheHelper
    {
        void BalanceUpdated();
        DateTime GetLastBalanceUpdated();
    }
}