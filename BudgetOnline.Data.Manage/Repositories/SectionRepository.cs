using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class SectionRepository : InternalRepository<Section, Types.Simple.Section>, ISectionRepository
	{
		public override Table<Section> Source
		{
			get { return DatabaseContext.Get().Sections; }
		}

		public Types.Simple.Section GetSection(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		public IEnumerable<Types.Simple.Section> GetSections()
		{
			return GetList(o => !o.IsDisabled)
				.OrderByDescending(o => o.CreatedWhen);
		}
	}
}
