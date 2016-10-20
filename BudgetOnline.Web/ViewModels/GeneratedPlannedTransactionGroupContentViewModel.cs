using System.Collections.Generic;

namespace BudgetOnline.Web.ViewModels
{
    public class GeneratedPlannedTransactionGroupContentViewModel
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        public decimal Sum { get; set; }
        public decimal SumBefore { get; set; }

        public IEnumerable<TransactionListItemViewModel> Items { get; set; }
    }
}