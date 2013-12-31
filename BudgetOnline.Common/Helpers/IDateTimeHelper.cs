using System;
using System.Collections.Generic;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.Common.Helpers
{
	public interface IDateTimeHelper
	{
		IEnumerable<DateTime> SequenceOfDates(DateTime from, DateTime to, TimePeriodTypes periodType);
        bool IsEqualDates(TimePeriodTypes periodType, DateTime date1, DateTime date2);
	}
}
