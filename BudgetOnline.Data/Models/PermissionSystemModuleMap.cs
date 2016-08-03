using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class PermissionSystemModuleMap
    {
        public IntRef Permission { get; set; }

        public IntRef SystemModule { get; set; }

        public DateTime CreatedWhen { get; set; }

        public UserRef CreatedUser { get; set; }
    }
}
