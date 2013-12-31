using System;

namespace BudgetOnline.BusinessLayer.Contracts
{
    public interface ITotalsOnDateUpdater
    {
        void UpdateData(DateTime date);
    }
}
