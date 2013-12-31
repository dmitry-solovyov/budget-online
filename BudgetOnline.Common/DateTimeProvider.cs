using System;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now(DateTimeKind kind = DateTimeKind.Local)
        {
            switch (kind)
            {
                case DateTimeKind.Utc:
                    return DateTime.UtcNow;
                default:
                    return DateTime.Now;
            }
        }

        public DateTime TimeLocal(DateTime timeUtc)
        {
            var d = DateTime.SpecifyKind(timeUtc, DateTimeKind.Utc);
            return d.ToLocalTime();
        }

        public DateTime TimeUtc(DateTime timeLocal)
        {
            var d = DateTime.SpecifyKind(timeLocal, DateTimeKind.Local);
            return d.ToUniversalTime();
        }

        public DateTime DateLocal(DateTime timeUtc)
        {
            return TimeLocal(timeUtc).Date;
        }

        public DateTime DateUtc(DateTime timeLocal)
        {
            return TimeUtc(timeLocal.Date);
        }
    }
}
