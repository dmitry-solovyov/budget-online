using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Infrastructure.Filters
{
    public class RequestAuthorizeAttribute : AuthorizationFilterAttribute, IAutofacAuthorizationFilter
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var currentSession = CurrentApiUserProvider.CurrentSession;
                if (currentSession != null)
                {
                    var currentPrincipal = new GenericPrincipal(new GenericIdentity(currentSession.User.Name), null);
                    Thread.CurrentPrincipal = currentPrincipal;

                    RefreshTicketUsage();

                    return;
                }
            }

            HandleUnauthorizedRequest(actionContext);
        }

        private void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, string.Empty);

            actionContext.Response.Headers.Add("WWW-Authenticate",
                string.Format("Token Scheme=\"budget\" location=\"{0}\"", actionContext.Request.RequestUri.DnsSafeHost));
        }

        private void RefreshTicketUsage()
        {
            CurrentApiUserProvider.UpdateTokenUsage();
        }
    }
}