using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class CurrentCurrencyRate
	{
		public int CurrencyId { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencySymbol { get; set; }
		public decimal Rate { get; set; }
		public DateTime Date { get; set; }
	}
}
