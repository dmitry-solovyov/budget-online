using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using BudgetOnline.Api.Infrastructure.IoC;

namespace BudgetOnline.Api
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                "API",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });

            routes.MapHttpRoute(
                "API",
                "{controller}/{id}",
                new { controller = "Home", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacInitializer.GetBuilder()));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}