using System.Collections.Generic;

namespace BudgetOnline.Common.Contracts
{
	public interface IValidated
	{
		IEnumerable<string> Errors();
	}
}
