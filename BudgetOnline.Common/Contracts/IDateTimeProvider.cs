using System;

namespace BudgetOnline.Common.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime Now(DateTimeKind kind = DateTimeKind.Local);
        
        DateTime TimeLocal(DateTime timeUtc);
        DateTime TimeUtc(DateTime timeLocal);

        DateTime DateLocal(DateTime timeUtc);
        DateTime DateUtc(DateTime timeLocal);
    }
}
