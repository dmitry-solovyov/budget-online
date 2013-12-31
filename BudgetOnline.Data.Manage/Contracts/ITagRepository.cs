using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
    public interface ITagRepository
	{
		IEnumerable<Tag> GetList(int sectionId);
		Tag Insert(Tag row);
		void Update(Tag row);

		Tag Get(int tagId);
	}
}
