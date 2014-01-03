using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Web.Infrastructure.Attributes;
using BudgetOnline.Web.Infrastructure.IoC;
using BudgetOnline.Web.Infrastructure.Minifier;

namespace BudgetOnline.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalExecuteActionFilterAttribute());
            filters.Add(new HandleViewErrorsAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Statistics", // Route name
                "statistics/transactions/{action}/{id}", // URL with parameters
                new { controller = "TransactionStatistics", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Statistics Charts", // Route name
                "charts/transactions/{action}/{id}", // URL with parameters
                new { controller = "TransactionCharts", action = "Index", id = UrlParameter.Optional });

            routes.MapHttpRoute(
                "API_short",
                "api/{controller}",
                new { controller = "Home" });


            routes.MapHttpRoute(
                "API",
                "api/{controller}/{id}",
                new { controller = "Home", id = UrlParameter.Optional });

            routes.MapHttpRoute(
                "API_full",
                "api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Welcome", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacInitializer.GetBuilder()));

            BundleTable.Bundles.EnableBootstrapBundle();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            //if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.Params[@"ReturnUrl"]))
            //	Response.Redirect(HttpContext.Current.Request.Path);
        }

        protected void Application_EndRequest()
        {
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            return base.GetVaryByCustomString(context, arg);
        }


        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started


            //Ensure SessionID in order to prevent the folloing exception
            //when the Application Pool Recycles
            //[HttpException]: Session state has created a session id, but cannot
            //    save it because the response was already flushed by 
            string sessionId = Session.SessionID;
        }


        private ILogWriter _logWriter;
        private ILogWriter LogWriter { get { return _logWriter = _logWriter ?? DependencyResolver.Current.GetService<ILogWriter>(); } }
    }
}