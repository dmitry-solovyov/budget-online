﻿using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class PermissionSystemModuleUserMap
    {
        public IntRef Permission { get; set; }
        
        public IntRef SystemModule { get; set; }
        
        public UserRef User { get; set; }

        public DateTime CreatedWhen { get; set; }

        public UserRef CreatedUser { get; set; }

        public DateTime? UpdatedWhen { get; set; }

        public UserRef UpdatedUser { get; set; }
    }
}
