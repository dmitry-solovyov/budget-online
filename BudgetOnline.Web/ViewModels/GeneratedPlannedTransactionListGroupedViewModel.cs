using System.Collections.Generic;
using System;

namespace BudgetOnline.Web.ViewModels
{
    public class GeneratedPlannedTransactionListGroupedViewModel
	{
        public DateTime Date { get; set; }

        public IEnumerable<TransactionStatisticViewModel> Statistics { get; set; }
	}
}