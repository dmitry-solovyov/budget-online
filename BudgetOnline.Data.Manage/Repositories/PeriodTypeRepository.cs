using System.Collections.Generic;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using PeriodType = BudgetOnline.Data.MSSQL.PeriodType;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class PeriodTypeRepository : InternalRepository<PeriodType, Types.Simple.PeriodType>, IPeriodTypeRepository
	{
		#region Overrides of InternalRepository<PeriodType,PeriodType>

		public override Table<PeriodType> Source
		{
			get { return DatabaseContext.Get().PeriodTypes; }
		}

		#endregion

		#region Implementation of IPeriodTypeRepository

		public IEnumerable<Types.Simple.PeriodType> GetList(int sectionId)
		{
			return GetList();
		}

		public Types.Simple.PeriodType GetDefault(int sectionId)
		{
			return base.GetSingle(o => o.IsDefault);
		}

		public void Update(Types.Simple.PeriodType row)
		{
			UpdateInternal(
				o => o.Id == row.Id,
				record =>
				{
					record.IsDefault = row.IsDefault;
					record.Name = row.Name;
					record.Description = row.Description;
				});
		}

		public Types.Simple.PeriodType Get(int id)
		{
			return base.GetSingle(o => o.Id == id);
		}

		#endregion
	}
}
