using System.ComponentModel.DataAnnotations;
using BudgetOnline.UI.PreCompiled.Attributes;

namespace BudgetOnline.UI.PreCompiled.Models.Editors
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
		public IdWithSelectList Currency { get; set; }
		[GridLayout(6)]
		public IdWithSelectList Account { get; set; }
	}
}