using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class AdminUser
    {
        public UserRef User { get; set; }

        public DateTime CreatedWhen { get; set; }

        public UserRef CreatedUser { get; set; }
    }
}
