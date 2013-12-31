using System;

namespace BudgetOnline.Contracts
{
	public interface IDateTimeProvider
	{
		DateTime Now();
		DateTime UtcNow();
		DateTime ConvertToLocal(DateTime utcTime);
	}
}
