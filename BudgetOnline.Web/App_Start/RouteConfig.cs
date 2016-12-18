using System.Web.Mvc;
using System.Web.Routing;

namespace BudgetOnline.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                "Statistics", // Route name
                "statistics/transactions/{action}/{id}", // URL with parameters
                new { controller = "TransactionStatistics", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Statistics Charts", // Route name
                "charts/transactions/{action}/{id}", // URL with parameters
                new { controller = "TransactionCharts", action = "Index", id = UrlParameter.Optional });

            //routes.MapHttpRoute(
            //    "API_short",
            //    "api/{controller}",
            //    new { controller = "Home" });

            //routes.MapHttpRoute(
            //    "API",
            //    "api/{controller}/{id}",
            //    new { controller = "Home", id = UrlParameter.Optional });

            //routes.MapHttpRoute(
            //    "API_full",
            //    "api/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Welcome", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}