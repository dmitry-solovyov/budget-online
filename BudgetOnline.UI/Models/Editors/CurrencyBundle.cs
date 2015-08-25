using System.ComponentModel.DataAnnotations;
using BudgetOnline.UI.Attributes;

namespace BudgetOnline.UI.Models.Editors
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

		[GridLayout(6)]
        [Required]
		public IdWithSelectList Currency { get; set; }
		[GridLayout(6)]
        [Required]
		public IdWithSelectList Account { get; set; }
	}
}