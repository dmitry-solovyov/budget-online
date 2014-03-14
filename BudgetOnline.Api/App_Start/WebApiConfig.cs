using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BudgetOnline.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "GetApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { action = "Get", id = RouteParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get), id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "PostApi",
                routeTemplate: "{controller}",
                defaults: new { action = "Create" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "PutApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { action = "Update" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );
        }
    }
}
