using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> GetList(int sectionId);
		Category GetDefault(int sectionId);
		Category Insert(Category row);
		void Update(Category row);

		Category Get(int categoryId);
	}
}
