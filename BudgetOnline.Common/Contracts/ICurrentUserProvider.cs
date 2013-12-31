namespace BudgetOnline.Common.Contracts
{
	public interface ICurrentUserProvider
	{
		int SectionId { get; }
        int UserId { get; }
	}
}
