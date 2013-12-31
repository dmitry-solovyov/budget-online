using System;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
    public interface IStatisticsTotalOnDateDetailsRepository
    {
        StatisticsTotalOnDateDetail Insert(StatisticsTotalOnDateDetail row);
        void Update(StatisticsTotalOnDateDetail row);
        void Delete(int id);

        void DeleteByParent(int parentId);
    }
}
