using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Contracts
{
    public interface ITransactionCalculator
    {
        IEnumerable<TransactionTotal> TotalsByDates(IEnumerable<Transaction> transactions, int targetCurrencyId);
    }
}
