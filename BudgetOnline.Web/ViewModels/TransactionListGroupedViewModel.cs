using System.Collections.Generic;

namespace BudgetOnline.Web.ViewModels
{
	public class TransactionListGroupedViewModel
	{
		public string Title { get; set; }

		public IEnumerable<TransactionListItemViewModel> Items { get; set; }
	}
}