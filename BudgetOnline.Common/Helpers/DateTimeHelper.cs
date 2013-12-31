using System;
using System.Collections.Generic;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.Common.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public IEnumerable<DateTime> SequenceOfDates(DateTime fromDate, DateTime toDate, TimePeriodTypes periodType)
        {
            if (fromDate > toDate)
                throw new ArgumentException("FromDate should be after ToDate");

            var currentDate = FirstDate(periodType, fromDate);

            while (DateInPeriod(periodType, toDate, currentDate))
            {
                yield return currentDate;

                currentDate = NextDate(periodType, currentDate);
            }
        }

        public bool IsEqualDates(TimePeriodTypes periodType, DateTime date1, DateTime date2)
        {
            switch (periodType)
            {
                case TimePeriodTypes.Monthly:
                    return date1.Month == date2.Month && date1.Year == date2.Year;

                default:
                    return date1.Date == date2.Date;
            }
        }

        private DateTime NextDate(TimePeriodTypes periodType, DateTime currentDate)
        {
            switch (periodType)
            {
                case TimePeriodTypes.Monthly:
                    return currentDate.AddMonths(1);

                default:
                    return currentDate.AddDays(1);
            }
        }

        private DateTime FirstDate(TimePeriodTypes periodType, DateTime fromDate)
        {
            switch (periodType)
            {
                case TimePeriodTypes.Monthly:
                    return new DateTime(fromDate.Year, fromDate.Month, 1);

                default:
                    return fromDate;
            }
        }

        private bool DateInPeriod(TimePeriodTypes periodType, DateTime toDate, DateTime currentDate)
        {
            switch (periodType)
            {
                case TimePeriodTypes.Monthly:
                    return new DateTime(currentDate.Year, currentDate.Month, 1) <= new DateTime(toDate.Year, toDate.Month, 1);

                default:
                    return currentDate <= toDate;
            }
        }
    }
}
