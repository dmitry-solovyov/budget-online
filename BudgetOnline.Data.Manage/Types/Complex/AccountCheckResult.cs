using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Types.Complex
{
    public class AccountCheckResult
    {
        public AccountCheckStatuses Status { get; set; }
        public User User { get; set; }
        public UserPassword UserPassword { get; set; }
        public UserConnect UserConnect { get; set; }
    }
}
