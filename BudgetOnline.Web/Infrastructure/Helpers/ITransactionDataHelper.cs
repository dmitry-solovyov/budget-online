using System.Collections.Generic;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
	public interface ITransactionDataHelper
	{
		string NormalizeTags(string tags);
		IEnumerable<string> ParseStringWithTags(string tags);
        decimal GetRealSumValue(TransactionTypes transactionType, decimal sum);
        decimal GetRealSumValue(int transactionTypeId, decimal sum);
	}
}