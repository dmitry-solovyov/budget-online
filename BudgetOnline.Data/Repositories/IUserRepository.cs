using System;
using BudgetOnline.Data.Contracts;
using BudgetOnline.Data.Models;

namespace BudgetOnline.Data.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}