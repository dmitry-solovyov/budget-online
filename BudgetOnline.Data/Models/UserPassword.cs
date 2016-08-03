using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class UserPassword
    {
        public Guid Id { get; set; }

        public GuidRef User { get; set; }

        public string Password { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedWhen { get; set; }

        public GuidRef CreatedUser { get; set; }
    }
}
