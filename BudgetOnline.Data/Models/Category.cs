using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public GuidRef Parent { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public bool IsDisabled { get; set; }
    }
}
