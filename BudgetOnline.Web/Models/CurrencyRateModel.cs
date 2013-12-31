using System;

namespace BudgetOnline.Web.Models
{
	public class CurrencyRateModel
	{
		public DateTime Date { get; set; }
		public int CurrencyId { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencySymbol { get; set; }
		public decimal Rate { get; set; }
	}
}