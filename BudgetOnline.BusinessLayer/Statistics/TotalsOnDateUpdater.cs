using System;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Statistics
{
    public class TotalsOnDateUpdater : ITotalsOnDateUpdater
    {
        public IDateTimeProvider DateTimeProvider { get; set; }
        public ICurrentUserProvider CurrentUserProvider { get; set; }
        public IStatisticsTotalOnDateRepository StatisticsTotalOnDateRepository { get; set; }
        public IStatisticsTotalOnDateDetailsRepository StatisticsTotalOnDateDetailsRepository { get; set; }
        public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }

        public void UpdateData(DateTime date)
        {
            var sectionId = CurrentUserProvider.SectionId;

            var totals = TransactionStatisticsRepository.GetTotalsByCurrencies(sectionId,
                                                                             new TransactionStatisticsSearchOptions
                                                                                 {
                                                                                     Date2 = DateTimeProvider.Now()
                                                                                 });

            var statisticsMain = StatisticsTotalOnDateRepository.Get(sectionId, date.Date);
            if (statisticsMain == null)
                statisticsMain = StatisticsTotalOnDateRepository.Insert(new StatisticsTotalOnDate
                        {
                            CreatedBy = CurrentUserProvider.UserId,
                            CreatedWhen = DateTimeProvider.Now(),
                            SectionId = sectionId,
                            TotalOnDate = date.Date
                        });
            else
                StatisticsTotalOnDateDetailsRepository.DeleteByParent(statisticsMain.Id);

            foreach (var transactionTotal in totals)
            {
                StatisticsTotalOnDateDetailsRepository.Insert(new StatisticsTotalOnDateDetail
                                                                  {
                                                                      CurrencyId = transactionTotal.CurrencyId.Value,
                                                                      Sum = transactionTotal.Sum,
                                                                      TotalOnDateId = statisticsMain.Id
                                                                  });
            }
        }
    }
}
