using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Types.Complex
{
    public class AccountCheckResult
    {
        public AccountCheckStatus Status { get; set; }
        //public UserPasswordInfo UserPasswordInfo { get; set; }
        public User User { get; set; }
        public UserPassword UserPassword { get; set; }
        public UserConnect UserConnect { get; set; }
    }
}
