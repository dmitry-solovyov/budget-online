using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
    public class Transaction
    {
        public int Id { get; set; }
        public int? LinkedId { get; set; }
        public bool? LinkedAsParent { get; set; }

        public int SectionId { get; set; }
        public int TransactionTypeId { get; set; }
        public int CurrencyId { get; set; }
        public int AccountId { get; set; }
        public int? CategoryId { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Sum { get; set; }
        public string Formula { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedWhen { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedWhen { get; set; }
        public int? UpdatedBy { get; set; }

        public string AccountNameSource { get; set; }
        public string AccountNameTarget { get; set; }

        public decimal SumSource { get; set; }
        public decimal? SumTarget { get; set; }

        public string CurrencyNameSource { get; set; }
        public string CurrencyNameTarget { get; set; }

        public string CurrencySymbolSource { get; set; }
        public string CurrencySymbolTarget { get; set; }

        public string CategoryName { get; set; }
    }
}
