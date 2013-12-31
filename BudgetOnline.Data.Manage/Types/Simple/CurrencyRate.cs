using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class CurrencyRate
	{
		public int Id { get; set; }
		public int SectionId { get; set; }
		public DateTime Date { get; set; }
		public int BaseCurrencyId { get; set; }
		public int TargetCurrencyId { get; set; }
		public decimal Rate { get; set; }
		public bool IsDisabled { get; set; }
		
		public int CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }

		public string BaseCurrencyName { get; set; }
        public string BaseCurrencySymbol { get; set; }
		public string TargetCurrencyName { get; set; }
        public string TargetCurrencySymbol { get; set; }
    }
}
