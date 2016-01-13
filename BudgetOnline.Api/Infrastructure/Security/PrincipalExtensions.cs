using System.Collections.Generic;
using ReMi.BusinessEntities.Auth;
using System.Linq;
using System.Security.Principal;

namespace ReMi.Api.Insfrastructure.Security
{
    public static class PrincipalExtensions
    {
        public static bool IsInRole(this IPrincipal principal, IEnumerable<Role> roles, Account account)
        {
            if (account == null || account.Role.Name.Equals("NotAuthenticated"))
                return false;

            return roles.Any(role => account.Role.Equals(role));
        }
    }
}