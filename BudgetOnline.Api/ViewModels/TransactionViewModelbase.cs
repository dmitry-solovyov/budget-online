using System;

namespace BudgetOnline.Api.ViewModels
{
    public class TransactionViewModelBase
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public bool IsDisabled { get; set; }
    }
}