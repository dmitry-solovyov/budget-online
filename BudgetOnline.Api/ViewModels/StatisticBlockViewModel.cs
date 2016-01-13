using System;
using System.Collections.Generic;

namespace BudgetOnline.Api.ViewModels
{
	public class StatisticItemsGroupViewModel
	{
		public string Title { get; set; }

		public IEnumerable<StatisticDetailItemViewModel> Items { get; set; }
	}

	public class StatisticDetailItemViewModel
	{
		public string Label { get; set; }
		public decimal Sum { get; set; }
		public string CurrencySymbol { get; set; }

		public Uri NavigationUrl { get; set; }
	}
}