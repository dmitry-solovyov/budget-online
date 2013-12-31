using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class SectionAdminRepository : InternalRepository<SectionAdmin, Types.Simple.SectionAdmin>, ISectionAdminRepository
	{
		public override Table<SectionAdmin> Source
		{
			get { return DatabaseContext.Get().SectionAdmins; }
		}

		public IEnumerable<Types.Simple.SectionAdmin> GetAdminsBySection(int id)
		{
			return GetList(o => o.Id == id);
		}
	}
}
