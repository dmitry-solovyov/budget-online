using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BudgetOnline.Api.Admin;
using BudgetOnline.Api.Admin.Infrastructure.IoC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Statup))]

namespace BudgetOnline.Api.Admin
{
    public class Statup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            var builder = AutofacInitializer.GetBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerRequest()
                .PropertiesAutowired();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            httpConfiguration.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            WebApiConfig.Register(httpConfiguration);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);
            app.UseWebApi(httpConfiguration);

            httpConfiguration.EnsureInitialized();
        }
    }
}