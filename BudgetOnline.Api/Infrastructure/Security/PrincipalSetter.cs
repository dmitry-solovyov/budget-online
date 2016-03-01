using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using BudgetOnline.Api.Common;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public class PrincipalSetter : IPrincipalSetter
    {
        public void SetPrincipal(IPrincipal principal, HttpActionContext actionContext)
        {
            Thread.CurrentPrincipal = principal;
            
            HttpContext.Current.User = principal;
            //actionContext.Request. Principal = principal;
        }
    }
}