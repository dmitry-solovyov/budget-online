namespace BudgetOnline.Data.QueryManagement
{
    public class QueryOperation
    {
        public string Field { get; set; }

        public Conditions Condition { get; set; }

        public object Value { get; set; }
    }
}
