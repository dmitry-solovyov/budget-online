using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BudgetOnline.BusinessLayer.Helpers;
using BudgetOnline.Common;
using BudgetOnline.Common.Logger;
using BudgetOnline.Data.Manage.Repositories;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Admin.Infrastructure.IoC
{
    public static class AutofacInitializer
    {
        public static IContainer GetBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerRequest();

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
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(ApiSessionProvider).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline."))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }

        private static void RegisterInfrustructure(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(typeof(ForceHttpsAttribute).Assembly)
            //    .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.Api.Infrastructure.") && !t.Name.Contains("Attribute"))
            //    .PropertiesAutowired()
            //    .AsImplementedInterfaces()
            //    .InstancePerRequest();

            //builder.Register(c => new RequestAuthorizeAttribute())
            //    .AsWebApiAuthorizationFilterFor<HomeController>()
            //    .PropertiesAutowired()
            //    .InstancePerRequest();
        }

        private static void RegisterBusinessLayerBindings(ContainerBuilder builder)
        {
            //builder.RegisterType<Dictionaries>().AsImplementedInterfaces()
            //    .PropertiesAutowired()
            //    .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(SettingsHelper).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.BusinessLayer."))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }

        private static void RegisterDataLayerBindings(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SettingRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}