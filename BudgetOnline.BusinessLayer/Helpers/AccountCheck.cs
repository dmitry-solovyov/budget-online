using System;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.BusinessLayer.Helpers
{
    internal class AccountCheck
    {
        public AccountCheckStatuses Status { get; set; }

        public DateTime ExpiredAfter { get; set; }
    }
}
