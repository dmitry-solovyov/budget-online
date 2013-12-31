using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IPeriodTypeRepository
	{
		IEnumerable<PeriodType> GetList(int sectionId);
		PeriodType GetDefault(int sectionId);
		PeriodType Insert(PeriodType row);
		void Update(PeriodType row);

		PeriodType Get(int id);
	}
}
