using System.Security.Principal;
using System.Web.Http.Controllers;

namespace BudgetOnline.Api.Common
{
    public interface IPrincipalSetter
    {
        void SetPrincipal(IPrincipal principal, HttpActionContext actionContext);
    }
}