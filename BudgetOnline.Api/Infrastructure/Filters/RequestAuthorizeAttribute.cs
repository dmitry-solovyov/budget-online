using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using BudgetOnline.Api.Infrastructure.Security;

namespace BudgetOnline.Api.Infrastructure.Filters
{
    public class RequestAuthorizeAttribute : AuthorizationFilterAttribute, IAutofacAuthorizationFilter
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var currentUser = CurrentApiUserProvider.CurrentUser;
                if (currentUser != null)
                {
                    var currentPrincipal = new GenericPrincipal(new GenericIdentity(currentUser.Name), null);
                    Thread.CurrentPrincipal = currentPrincipal;

                    RefreshTicketUsage();

                    return;
                }
            }

            HandleUnauthorizedRequest(actionContext);
        }

        private void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Headers.Add("WWW-Authenticate",
            "Basic Scheme='eBudget' location='http://budget/session/login'");
        }

        private void RefreshTicketUsage()
        {
            CurrentApiUserProvider.UpdateTokenUsage();
        }
    }
}