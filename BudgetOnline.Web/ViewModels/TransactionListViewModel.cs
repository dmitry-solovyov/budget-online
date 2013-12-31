using System.Collections.Generic;
using System.Linq;

namespace BudgetOnline.Web.ViewModels
{
	public class TransactionListViewModel
	{
		public TransactionListSearchViewModel Search { get; set; }
		public IEnumerable<TransactionListGroupedViewModel> Groups { get; set; }
		public IEnumerable<TransactionStatisticViewModel> Totals { get; set; }

		public TransactionListViewModel()
		{
			Groups = Enumerable.Empty<TransactionListGroupedViewModel>();
			Totals = Enumerable.Empty<TransactionStatisticViewModel>();
		}
	}
}