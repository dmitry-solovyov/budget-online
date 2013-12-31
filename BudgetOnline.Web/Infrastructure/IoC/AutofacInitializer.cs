using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BudgetOnline.BusinessLayer.Helpers;
using BudgetOnline.Common;
using BudgetOnline.Common.Logger;
using BudgetOnline.Data.Manage.Repositories;

namespace BudgetOnline.Web.Infrastructure.IoC
{
    public class AutofacInitializer
    {
        public static IContainer GetBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerDependency();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerDependency();

            RegisterInfrustructure(builder);
            RegisterCommonBindings(builder);
            RegisterBusinessLayerBindings(builder);
            RegisterDataLayerBindings(builder);

            return builder.Build();
        }

        private static void RegisterCommonBindings(ContainerBuilder builder)
        {
            builder.RegisterType<LogWriter>().AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(DateTimeProvider).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.Common"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private static void RegisterInfrustructure(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Binders.CustomViewModelBinder).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.Web.Infrastructure."))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private static void RegisterBusinessLayerBindings(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SettingsHelper).Assembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.StartsWith("BudgetOnline.BusinessLayer."))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private static void RegisterDataLayerBindings(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SettingRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}