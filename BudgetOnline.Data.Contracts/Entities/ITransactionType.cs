namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ITransactionType
	{
		int Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
	}
}
