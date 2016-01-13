using System;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Security.Api
{
    public class SessionInfo
    {
        public int Id { get; set; }

        public DateTime ExpiresWhen { get; set; }

        public User User { get; set; }
    }
}