using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class CurrencyRateListViewModel
	{
		public int Id { get; set; }
		[DisplayName("Из валюты")]
        public string BaseCurrencyName { get; set; }
        [DisplayName("В валюту")]
        public string TargetCurrencyName { get; set; }
		[DisplayName("Курс")]
		public decimal Rate { get; set; }
		[DisplayName("Заблокирована")]
		public bool IsDisabled { get; set; }

		[DisplayName("Дата")]
		public DateTime Date { get; set; }

		public DateTime CreatedWhen { get; set; }
		public DateTime? UpdatedWhen { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }
	}
}