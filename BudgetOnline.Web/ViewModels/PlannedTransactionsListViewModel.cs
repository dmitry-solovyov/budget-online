using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetOnline.Web.ViewModels
{
    public class PlannedTransactionsListViewModel
    {
        public IEnumerable<PlannedTransactionsListItemViewModel> Items { get; set; }
    }
}