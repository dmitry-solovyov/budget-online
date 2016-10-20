using System.Collections.Generic;

namespace BudgetOnline.Web.ViewModels
{
    public class PlannedTransactionsListViewModel
    {
        public IEnumerable<PlannedTransactionsListItemViewModel> Items { get; set; }
    }
}