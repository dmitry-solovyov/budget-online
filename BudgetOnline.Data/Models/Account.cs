using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class AccountRecord
    {
        public Guid Id { get; set; }

        public GuidRef Section { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public int SortOrder { get; set; }

        public bool IsExternal { get; set; }

        public bool IsDisabled { get; set; }
        
        public DateTime CreatedWhen { get; set; }

        public UserRef CreatedUser { get; set; }

        public DateTime? UpdatedWhen { get; set; }

        public UserRef UpdatedUser { get; set; }
    }
}
