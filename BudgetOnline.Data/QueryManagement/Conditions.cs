namespace BudgetOnline.Data.QueryManagement
{
    public enum Conditions
    {
        NotSet = 0,
        Equal,
        NotEqual,
        GraterThen,
        GraterThenOeEqual,
        LessThen,
        LessThenOrEqual,
        Contains,
        Between,
        IsEmpty,
        NotEmpty
    }
}
