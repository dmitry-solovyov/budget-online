using System;
using BudgetOnline.Data.Models;

namespace BudgetOnline.Data.Contracts
{
    public interface IUserSessionRepository : IRepository<UserSession>
    {
        void MarkPreviousTokensDisabled(Guid userId);
    }
}
