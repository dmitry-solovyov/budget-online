using BudgetOnline.Common.Enums;

namespace BudgetOnline.Data.Models
{
    public class Permission
    {
        public Permissions Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }
    }
}
