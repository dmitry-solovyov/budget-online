using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class PlannedTransaction
	{
		public int Id { get; set; }

		public DateTime FromDate { get; set; }
		public DateTime? ToDate { get; set; }

		public int SectionId { get; set; }
		public int TransactionTypeId { get; set; }
		public int CurrencyId { get; set; }
		public int? AccountId { get; set; }
		public int? CategoryId { get; set; }
		public int PeriodTypeId { get; set; }

		public decimal Amount { get; set; }
		public decimal Sum { get; set; }
		public string Tags { get; set; }
		public string Description { get; set; }
		
		public bool IsDisabled { get; set; }

		public DateTime CreatedWhen { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
		public int? UpdatedBy { get; set; }

		public string AccountName { get; set; }
		public string CategoryName { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencySymbol { get; set; }
		public string PeriodTypeName { get; set; }
	}
}
