using System;

namespace BudgetOnline.Web.ViewModels
{
    public class PlannedTransactionsSearchViewModel
    {
        public string Text { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public int? PagesCount { get; set; }
    }
}