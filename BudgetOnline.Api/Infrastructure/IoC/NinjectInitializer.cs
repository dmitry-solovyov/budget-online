using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.DataManage.Contracts;
using BudgetOnline.BusinessLayer.DataManage.Helpers;
using BudgetOnline.Common;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Repositories;
using BudgetOnline.Web.Infrastructure.Core;
using BudgetOnline.Web.Infrastructure.Helpers;
using BudgetOnline.Web.Infrastructure.Security;
using Ninject;
using Ninject.Syntax;

namespace BudgetOnline.Web.Infrastructure.IoC
{
	public class NinjectInitializer : IDependencyResolver
	{
		private readonly IKernel _kernel;

		public NinjectInitializer()
		{
			_kernel = new StandardKernel();

			AddBindings();
			AddDataLayerBindings();
			AddBusinessLayerBindings();
		}

		private void AddBindings()
		{
			Bind<ILogWriter>().To<Common.Logger.LogWriter>();
			Bind<ISessionWrapper>().To<SessionWrapper>();
			Bind<ICacheWrapper>().To<CacheWrapper>()
							.OnActivation((context, obj) =>
							{
								obj.Log = (ILogWriter)context.Kernel.GetService(typeof(ILogWriter));
							});

			Bind<IAuthenticationDataHelper>().To<AuthenticationDataHelper>()
				.OnActivation((context, obj) =>
				{
					obj.UserRepository = (IUserRepository)context.Kernel.GetService(typeof(IUserRepository));
					obj.UserPasswordRepository = (IUserPasswordRepository)context.Kernel.GetService(typeof(IUserPasswordRepository));
					obj.SettingsHelper = (ISettingsHelper)context.Kernel.GetService(typeof(ISettingsHelper));
					obj.UserConnectRepository = (IUserConnectRepository)context.Kernel.GetService(typeof(IUserConnectRepository));
					obj.Log = (ILogWriter)context.Kernel.GetService(typeof(ILogWriter));
				});
			Bind<IAuthProvider>().To<FormsAuthProvider>()
				.OnActivation((context, obj) =>
								{
									obj.AuthenticationDataHelper = (IAuthenticationDataHelper)context.Kernel.GetService(typeof(IAuthenticationDataHelper));
									obj.SessionWrapper = (ISessionWrapper)context.Kernel.GetService(typeof(ISessionWrapper));
								});
			Bind<IMembershipHelper>().To<MembershipHelper>()
				.OnActivation((context, obj) =>
								{
									obj.UserPermissionRepository = (IUserPermissionRepository)context.Kernel.GetService(typeof(IUserPermissionRepository));
									obj.PermissionRepository = (IPermissionRepository)context.Kernel.GetService(typeof(IPermissionRepository));
								});

			Bind<ICachedStorage>().To<CachedStorage>();
			Bind<IDictionaries>().To<CachedDictionaries>();
			Bind<ITransactionDataHelper>().To<TransactionDataHelper>();
			Bind<IDateTimeProvider>().To<DateTimeProvider>();


			//// create the email settings object 
			//EmailSettings emailSettings = new EmailSettings { 
			//    WriteAsFile = bool.Parse( 
			//        ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
			//}; 

			//Bind<IOrderProcessor>() 
			//    .To<EmailOrderProcessor>() 
			//    .WithConstructorArgument("settings", emailSettings); 
		}

		private void AddBusinessLayerBindings()
		{

			Bind<IPlannedTransactionCalculator>().To<PlannedTransactionCalculator>()
				.OnActivation((context, obj) =>
								{
									obj.PeriodTypeRepository = (IPeriodTypeRepository)context.Kernel.GetService(typeof(IPeriodTypeRepository));
									obj.TransactionStatisticsRepository = (ITransactionStatisticsRepository)context.Kernel.GetService(typeof(ITransactionStatisticsRepository));
									obj.PlannedTransactionRepository = (IPlannedTransactionRepository)context.Kernel.GetService(typeof(IPlannedTransactionRepository));
									obj.CurrencyRateCalculator = (ICurrencyRateCalculator)context.Kernel.GetService(typeof(ICurrencyRateCalculator));
									obj.DateTimeProvider = (IDateTimeProvider)context.Kernel.GetService(typeof(IDateTimeProvider));
								});

			Bind<ISettingsHelper>().To<SettingsHelper>()
				.OnActivation((context, obj) =>
				{
					obj.CacheWrapper = (ICacheWrapper)context.Kernel.GetService(typeof(ICacheWrapper));
					obj.SettingRepository = (ISettingRepository)context.Kernel.GetService(typeof(ISettingRepository));
				});

			
			Bind<ICurrencyRateCalculator>().To<CurrencyRateCalculator>()
				.OnActivation((context, obj) =>
				{
					obj.DateTimeProvider = (IDateTimeProvider)context.Kernel.GetService(typeof(IDateTimeProvider));
					obj.CurrencyRateRepository = (ICurrencyRateRepository)context.Kernel.GetService(typeof(ICurrencyRateRepository));
					obj.CurrencyRepository = (ICurrencyRepository)context.Kernel.GetService(typeof(ICurrencyRepository));
				});
		}

		private void AddDataLayerBindings()
		{
			Bind<ISettingRepository>().To<SettingRepository>();
			Bind<IUserRepository>().To<UserRepository>();
			Bind<IUserPasswordRepository>().To<UserPasswordRepository>();
			Bind<IUserConnectRepository>().To<UserConnectRepository>();
			Bind<ITransactionRepository>().To<TransactionRepository>();
			Bind<ICurrencyRepository>().To<CurrencyRepository>();
			Bind<IAccountRepository>().To<AccountRepository>();
			Bind<ITagRepository>().To<TagRepository>();
			Bind<ITransactionLinkRepository>().To<TransactionLinkRepository>();
			Bind<ICategoryRepository>().To<CategoryRepository>();
			Bind<ITransactionTagRepository>().To<TransactionTagRepository>();
			Bind<IPlannedTransactionRepository>().To<PlannedTransactionRepository>();
			Bind<IPeriodTypeRepository>().To<PeriodTypeRepository>();
			Bind<ICurrencyRateRepository>().To<CurrencyRateRepository>();
			Bind<IPermissionRepository>().To<PermissionRepository>();
			Bind<IUserPermissionRepository>().To<UserPermissionRepository>();
			Bind<ITransactionStatisticsRepository>().To<TransactionStatisticsRepository>();
		}

		public object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType);
		}

		public IBindingToSyntax<T> Bind<T>()
		{
			return _kernel.Bind<T>();
		}

		public IKernel Kernel
		{
			get { return _kernel; }
		}
	}
}