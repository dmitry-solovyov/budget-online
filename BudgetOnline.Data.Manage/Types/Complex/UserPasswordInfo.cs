using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Types.Complex
{
    public class UserPasswordInfo
    {
        public User User { get; set; }
        public UserPassword UserPassword { get; set; }
    }
}
