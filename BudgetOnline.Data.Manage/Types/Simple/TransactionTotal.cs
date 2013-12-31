using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class TransactionTotal
	{
		public int Count { get; set; }

		public int SectionId { get; set; }

		public int? AccountId { get; set; }
		public string AccountName { get; set; }
		public bool? AccountIsExternal { get; set; }

		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }

		public DateTime? Date { get; set; }
		public bool SumNegative
		{
			get { return Sum < 0; }
		}
		public decimal Sum { get; set; }

		public int? CurrencyId { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencySymbol { get; set; }

		public int[] RelatedTransactionIds { get; set; }
	}
}
