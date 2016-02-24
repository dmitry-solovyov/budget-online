namespace BudgetOnline.Api.ViewModels
{
    public class TransactionMoveViewModel : TransactionViewModelBase
    {
        public string Account { get; set; }

        public string AccountFrom { get; set; }

        public string AccountTo { get; set; }

        public string Currency { get; set; }

        public string CurrencyFrom { get; set; }

        public string CurrencyTo { get; set; }

        public decimal? Sum { get; set; }

        public decimal? SumFrom { get; set; }

        public decimal? SumTo { get; set; }
    }
}