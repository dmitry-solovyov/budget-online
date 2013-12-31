using System;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using StatisticsTotalOnDateDetail = BudgetOnline.Data.MSSQL.StatisticsTotalOnDateDetail;

namespace BudgetOnline.Data.Manage.Repositories
{
    public class StatisticsTotalOnDateDetailRepository : InternalRepository<StatisticsTotalOnDateDetail, Types.Simple.StatisticsTotalOnDateDetail>, IStatisticsTotalOnDateDetailsRepository
    {
        #region Overrides of InternalRepository<StatisticsTotalOnDateDetail>

        public override Table<StatisticsTotalOnDateDetail> Source
        {
            get { return DatabaseContext.Get().StatisticsTotalOnDateDetails; }
        }

        #endregion

        #region Implementation of IStatisticsTotalOnDateRepository

        public void Update(Types.Simple.StatisticsTotalOnDateDetail row)
        {
            UpdateInternal(o => o.Id == row.Id,
                                o =>
                                {
                                    o.CurrencyId = row.CurrencyId;
                                    o.Sum = row.Sum;
                                });
        }

        public void Delete(int id)
        {
            base.Delete(o => o.Id == id);
        }

        public void DeleteByParent(int parentId)
        {
            base.Delete(o => o.TotalOnDateId == parentId);
        }

        #endregion
    }
}
