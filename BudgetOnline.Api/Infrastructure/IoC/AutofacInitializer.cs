using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BudgetOnline.Api.Controllers;
using BudgetOnline.Api.Infrastructure.Filters;
using BudgetOnline.BusinessLayer.Helpers;
using BudgetOnline.Common;
using BudgetOnline.Common.Logger;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Repositories;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Infrastructure.IoC
{
    public class AutofacInitializer
    {
        public static IContainer GetBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerApiRequest();

            RegisterInfrustructure(builder);
            RegisterCommonBindings(builder);
            RegisterBusinessLayerBindings(builder);
            RegisterDataLayerBindings(builder);

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            return builder.Build();
        }

        private static void RegisterCommonBindings(ContainerBuilder builder)
        {
            builder.RegisterType<LogWriter>().AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(DateTimeProvider).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.Common"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerApiRequest();

            builder.RegisterAssemblyTypes(typeof(ApiSessionProvider).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline."))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerApiRequest();
        }

        private static void RegisterInfrustructure(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ForceHttpsAttribute).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.Api.Infrastructure.") && !t.Name.Contains("Attribute"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerApiRequest();

            builder.Register(c => new RequestAuthorizeAttribute())
                .AsWebApiAuthorizationFilterFor<HomeController>()
                .PropertiesAutowired()
                .InstancePerApiRequest();
        }

        private static void RegisterBusinessLayerBindings(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SettingsHelper).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.BusinessLayer."))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerApiRequest();
        }

        private static void RegisterDataLayerBindings(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SettingRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerApiRequest();
        }
    }
}