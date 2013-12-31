using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Web.Models
{
	public class CurrencyBundle
	{
		public CurrencyBundle()
		{
			Account = new IdWithSelectList();
			Currency = new IdWithSelectList();
		}

		public string Formula { get; set; }

		[UIHint("Currency")]
		public decimal Sum { get; set; }

		public IdWithSelectList Currency { get; set; }
		public IdWithSelectList Account { get; set; }
	}
}