using System;

namespace BudgetOnline.Data.Manage.Types.Complex
{
	public class TransactionJoined
	{
		public int Id { get; set; }

		public int AccountOutId { get; set; }
		public string AccountOutName { get; set; }
		public bool AccountOutIsExternal { get; set; }
		public int CurrencyOutId { get; set; }
		public string CurrencyOutName { get; set; }
		public string CurrencyOutSymbol { get; set; }
		public bool SumOutNegative { get; set; }
		public decimal SumOut { get; set; }

        public int TransactionTypeId { get; set; }
		public int? AccountInId { get; set; }
		public string AccountInName { get; set; }
		public bool? AccountInIsExternal { get; set; }
		public int? CurrencyInId { get; set; }
		public string CurrencyInName { get; set; }
		public string CurrencyInSymbol { get; set; }
		public bool? SumInNegative { get; set; }
		public decimal? SumIn { get; set; }

		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }

		public DateTime Date { get; set; }

		public decimal Amount { get; set; }
		public string Tags { get; set; }
		public string Description { get; set; }
		public bool IsDisabled { get; set; }

		public int CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }

	}
}
