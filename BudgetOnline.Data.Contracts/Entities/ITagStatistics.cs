namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ITagStatistics
	{
		string Tag { get; set; }
		int Hits { get; set; }
		decimal Sum { get; set; }
	}
}
