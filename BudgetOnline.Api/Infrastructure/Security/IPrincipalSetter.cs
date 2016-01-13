using System.Security.Principal;
using System.Web.Http.Controllers;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public interface IPrincipalSetter
    {
        void SetPrincipal(IPrincipal principal, HttpActionContext actionContext);
    }
}