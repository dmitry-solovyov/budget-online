using System;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class UserSession
    {
        public GuidRef User { get; set; }

        public Guid UserPasswordId { get; set; }

        public UserSessionStatuses UserSessionStatus { get; set; }

        public DateTime CreatedWhen { get; set; }
    }
}
