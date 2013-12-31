using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ITransactionStatisticsRepository 
	{
		IEnumerable<TransactionTotal> GetStatistictsByTag(int sectionId, TransactionStatisticsSearchOptions options);
        IEnumerable<TransactionTotal> GetStatistictsByCurrency(int sectionId, TransactionStatisticsSearchOptions options);
        IEnumerable<TransactionTotal> GetStatistictsByAccount(int sectionId, TransactionStatisticsSearchOptions options);
		/// <summary>
		/// Monthy statistics by categories
		/// </summary>
		/// <param name="sectionId"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		IEnumerable<TransactionTotal> GetStatistictsByCategory(int sectionId, TransactionStatisticsSearchOptions options);

		/// <summary>
		/// Balance by currencies on current date
		/// </summary>
		/// <param name="sectionId"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		IEnumerable<TransactionTotal> GetTotalsByCurrencies(int sectionId, TransactionStatisticsSearchOptions options);
		IEnumerable<TransactionTotal> GetTotalsByAccounts(int sectionId, TransactionStatisticsSearchOptions options);
		IEnumerable<TransactionTotal> GetTotalsByCategories(int sectionId, TransactionStatisticsSearchOptions options);
	}
}
