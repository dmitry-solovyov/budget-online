using System;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public interface IPermissionChecker
    {
        PermissionStatus CheckCommandPermission(Type commandType, Account account);
        PermissionStatus CheckQueryPermission(Type queryType, Account account);

        PermissionStatus CheckRule(Account account, object data);
    }
}