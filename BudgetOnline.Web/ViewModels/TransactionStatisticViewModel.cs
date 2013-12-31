namespace BudgetOnline.Web.ViewModels
{
	public class TransactionStatisticViewModel
	{
		public string CurrencyName { get; set; }
		public string CurrencySymbol { get; set; }
		public int CurrencyId { get; set; }
		public decimal Balance { get; set; }
		public decimal BalancePositive { get; set; }
		public decimal BalanceNegative { get; set; }
		public decimal BalanceAccumulative { get; set; }
        public decimal Rate { get; set; }
	}
}