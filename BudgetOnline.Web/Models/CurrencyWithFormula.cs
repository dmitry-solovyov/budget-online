using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Web.Models
{
	public class CurrencyWithFormula
	{
		public string Formula { get; set; }

		[UIHint("Currency")]
		public decimal Sum { get; set; }
	}
}