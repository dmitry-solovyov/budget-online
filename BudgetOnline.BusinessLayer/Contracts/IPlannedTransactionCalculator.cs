using System;
using System.Collections.Generic;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Contracts
{
	public interface IPlannedTransactionCalculator
	{
		IEnumerable<TransactionTotal> GenerateActual(int sectionId, PlannedTransactionSearchOptions options, int? currencyId, bool useActualTotals);
		IEnumerable<TransactionTotal> GenerateActualByPlannedTransaction(int sectionId, DateTime fromDate, DateTime toDate, int transactionId);
		IEnumerable<PlannedTransaction> FindPlansRelatedToDate(int sectionId, DateTime fromDate, DateTime toDate);
		IEnumerable<TransactionTotal> GroupBy(IEnumerable<TransactionTotal> transactions, TransactionTotalGroupTypes groupType);
	}

	public enum TransactionTotalGroupTypes
	{
		Month,
		MonthSign,
		MonthCurrencySign,
		MonthAccountSign,
		Date,
		DateSign,
		DateCurrency,
		DateCurrencySign,
		DateAccount,
	}
}
