namespace BudgetOnline.Api.ViewModels
{
    public class TransactionMoveViewModel : TransactionViewModelBase
    {
        public string AccountFrom { get; set; }

        public string AccountTo { get; set; }

        public string Currency { get; set; }

        public decimal Sum { get; set; }
    }
}