using System;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.Data.Manage
{
	public class TransactionStatisticsSearchOptions : TransactionSearchOptions
	{
		public TimePeriodTypes? GroupBy { get; set; }
	}
}
