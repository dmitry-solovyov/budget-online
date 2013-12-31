using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ISectionAdminRepository
	{
		IEnumerable<SectionAdmin> GetAdminsBySection(int sectionId);
	}
}
