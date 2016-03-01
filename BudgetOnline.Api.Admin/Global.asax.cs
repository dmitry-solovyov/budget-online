using System.Web.Http;
using Autofac.Integration.WebApi;
using BudgetOnline.Api.Admin.Infrastructure;
using BudgetOnline.Api.Admin.Infrastructure.IoC;

namespace BudgetOnline.Api.Admin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperWebApiConfiguration.Configure();

            //GlobalConfiguration.Configuration.Filters.AddRange(FilterConfig.GlobalFilters());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            var resolver = new AutofacWebApiDependencyResolver(AutofacInitializer.GetBuilder());
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }
    }
}
