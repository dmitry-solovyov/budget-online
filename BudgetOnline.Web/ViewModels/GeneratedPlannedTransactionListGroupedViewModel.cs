using System;
using System.Collections.Generic;

namespace BudgetOnline.Web.ViewModels
{
    public class GeneratedPlannedTransactionListGroupedViewModel
	{
        public DateTime Date { get; set; }

        public IEnumerable<TransactionStatisticViewModel> Statistics { get; set; }
	}
}