using System;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
    public interface IStatisticsTotalOnDateRepository
    {
        StatisticsTotalOnDate Get(int id);
        StatisticsTotalOnDate Get(int sectionId, DateTime date);

        StatisticsTotalOnDate Insert(StatisticsTotalOnDate row);
        void Delete(int id);
    }
}
