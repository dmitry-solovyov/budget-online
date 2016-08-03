using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class Section
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedWhen { get; set; }

        public UserRef CreatedUser { get; set; }

        public DateTime? UpdatedWhen { get; set; }

        public UserRef UpdatedUser { get; set; }
    }
}
