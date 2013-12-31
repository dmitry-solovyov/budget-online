using System.Collections.Generic;
using System.Linq;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Helpers
{
    public class TransactionCalculator : ITransactionCalculator
    {
        public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }
        public IDictionaries Dictionaries { get; set; }

        public IEnumerable<TransactionTotal> TotalsByDates(IEnumerable<Transaction> transactions, int targetCurrencyId)
        {
            var converted = CurrencyRateCalculator.ConvertCurrency(transactions, targetCurrencyId).ToList();

            var currency = Dictionaries.Currencies().First(o => o.Id.Equals(targetCurrencyId));

            return converted.GroupBy(o => o.Date.Date)
                .Select(o => new TransactionTotal
                {
                    Date = o.Key,
                    Sum = o.Sum(x => x.Sum),
                    CurrencyId = currency.Id,
                    CurrencyName = currency.Name,
                    CurrencySymbol = currency.Symbol
                }).ToList();
        }
    }
}
