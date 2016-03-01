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

            var builder = new ContainerBuilder();

            var container = builder.Build();
            
            var dependencyResolver = new AutofacWebApiDependencyResolver(AutofacInitializer.GetBuilder());
            httpConfiguration.DependencyResolver = dependencyResolver;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            WebApiConfig.Register(httpConfiguration);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}