namespace BudgetOnline.Data.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public bool IsDisabled { get; set; }
    }
}
