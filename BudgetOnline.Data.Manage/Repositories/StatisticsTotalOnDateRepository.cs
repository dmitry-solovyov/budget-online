using System;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using StatisticsTotalOnDate = BudgetOnline.Data.MSSQL.StatisticsTotalOnDate;

namespace BudgetOnline.Data.Manage.Repositories
{
    public class StatisticsTotalOnDateRepository : InternalRepository<StatisticsTotalOnDate, Types.Simple.StatisticsTotalOnDate>, IStatisticsTotalOnDateRepository
    {
        #region Overrides of InternalRepository<StatisticsTotalOnDate>

        public override Table<StatisticsTotalOnDate> Source
        {
            get { return DatabaseContext.Get().StatisticsTotalOnDates; }
        }

        #endregion

        #region Implementation of IStatisticsTotalOnDateRepository

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Types.Simple.StatisticsTotalOnDate Get(int id)
        {
            return base.GetSingle(o => o.Id == id);
        }

        public Types.Simple.StatisticsTotalOnDate Get(int sectionId, DateTime date)
        {
            return base.GetSingle(o => o.SectionId == sectionId && o.TotalOnDate == date);
        }

        #endregion
    }
}
