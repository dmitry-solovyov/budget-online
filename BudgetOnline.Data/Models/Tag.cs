using System;

namespace BudgetOnline.Data.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public bool IsDisabled { get; set; }
    }
}
