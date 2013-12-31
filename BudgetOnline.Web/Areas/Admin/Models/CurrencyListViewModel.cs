using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class CurrencyListViewModel
	{
		public int Id { get; set; }
		[DisplayName("Валюта")]
		public string Name { get; set; }
		[DisplayName("Символ")]
		public string Symbol { get; set; }
		[DisplayName("Заблокирована")]
		public bool IsDisabled { get; set; }
		[DisplayName("По-умолчанию")]
		public bool IsDefault { get; set; }

		public decimal CurrentRate { get; set; }

		public DateTime CreatedWhen { get; set; }
		public DateTime? UpdatedWhen { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }
	}
}