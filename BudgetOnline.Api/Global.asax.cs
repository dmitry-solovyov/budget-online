using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using BudgetOnline.Api.Infrastructure.IoC;

namespace BudgetOnline.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperWebApiConfiguration.Configure();

            GlobalConfiguration.Configuration.Filters.AddRange(FilterConfig.GlobalFilters());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            var resolver = new AutofacWebApiDependencyResolver(AutofacInitializer.GetBuilder());
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }
    }
}