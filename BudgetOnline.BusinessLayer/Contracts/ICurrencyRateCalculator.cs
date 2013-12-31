using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Contracts
{
	public interface ICurrencyRateCalculator
	{
		CurrentCurrencyRate GetRate(int fromCurrencyId, int toCurrencyId);
		IEnumerable<Transaction> ConvertCurrency(IEnumerable<Transaction> transactions, int targetCurrencyId);
		IEnumerable<TransactionTotal> ConvertCurrency(IEnumerable<TransactionTotal> transactions, int targetCurrencyId);
	}
}
