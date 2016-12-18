using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
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

        private ILogWriter _logWriter;
        //private ILogWriter LogWriter { get { return _logWriter = _logWriter ?? DependencyResolver.Current.GetService<ILogWriter>(); } }
    }
}