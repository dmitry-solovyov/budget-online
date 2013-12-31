using System.Collections.Generic;
using BudgetOnline.Data.Contracts.Entities;

namespace BudgetOnline.Data.Contracts.Repositories
{
	public interface ISectionRepository
	{
		ISection GetSection(int id);
		IEnumerable<ISection> GetSections();
	}
}
