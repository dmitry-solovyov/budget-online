using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetOnline.Web.Models
{
	public class StatisticBlockViewModel
	{
		public string Title { get; set; }
		public string Tooltip { get; set; }

		public Uri HelpUrl { get; set; }
		public IEnumerable<StatisticBlockItemsGroupViewModel> Groups { get; set; }

		public int NumberOfGroups
		{
			get
			{
				if (Groups == null)
					return 0;

				return Groups.Count();
			}
		}
	}

	public class StatisticBlockItemsGroupViewModel
	{
        public int Id { get; set; }
		public string Title { get; set; }
		public string Tooltip { get; set; }

		public IEnumerable<StatisticDetailItemViewModel> Items { get; set; }
	}

	public class StatisticDetailItemViewModel
	{
		public string Tooltip{ get; set; }
		public string Label { get; set; }
		public decimal Sum { get; set; }
		public string CurrencySymbol { get; set; }

		public Uri NavigationUrl { get; set; }
	}
}