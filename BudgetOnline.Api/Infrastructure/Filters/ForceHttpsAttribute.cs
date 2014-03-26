using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;

namespace BudgetOnline.Api.Infrastructure.Filters
{
    public class ForceHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                const string html = "<p>Https is required</p>";
                const int httpsPort = 443;

                if (request.Method.Method == "GET")
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.Found, string.Empty);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                    var httpsNewUri = new UriBuilder(request.RequestUri)
                                          {
                                              Scheme = Uri.UriSchemeHttps,
                                              Port = httpsPort
                                          };

                    actionContext.Response.Headers.Location = httpsNewUri.Uri;
                }
                else
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound, string.Empty);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }
            }
        }
    }
}