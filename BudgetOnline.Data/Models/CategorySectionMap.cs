using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class CategorySectionMap
    {
        public GuidRef Section { get; set; }

        public GuidRef Category { get; set; }

        public DateTime CreatedWhen { get; set; }

        public UserRef CreatedUser { get; set; }

        public DateTime? UpdatedWhen { get; set; }

        public UserRef UpdatedUser { get; set; }
    }
}
