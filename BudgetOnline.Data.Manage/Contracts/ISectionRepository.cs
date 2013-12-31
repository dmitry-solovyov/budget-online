using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ISectionRepository
	{
		Section GetSection(int id);
		IEnumerable<Section> GetSections();
	}
}
