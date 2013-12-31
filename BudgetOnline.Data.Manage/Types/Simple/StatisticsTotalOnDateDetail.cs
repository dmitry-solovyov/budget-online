namespace BudgetOnline.Data.Manage.Types.Simple
{
    public class StatisticsTotalOnDateDetail
    {
        public int Id { get; set; }
        public int TotalOnDateId { get; set; }
        public decimal Sum { get; set; }

        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
