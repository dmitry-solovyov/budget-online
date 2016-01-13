using System.Collections.Generic;
using System.Web.Http.Controllers;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public interface IAuthorizationManager
    {
        Account Authenticate(HttpActionContext actionContext);
        //bool IsAuthorized(IEnumerable<Role> roles);
    }
}
