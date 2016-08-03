using BudgetOnline.Common.Enums;

namespace BudgetOnline.Data.Models
{
    public class SystemModule
    {
        public SystemModules Id { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }
    }
}
