using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Contracts;
using BudgetOnline.Web.Infrastructure;
using BudgetOnline.Web.Infrastructure.Attributes;
using BudgetOnline.Web.Infrastructure.Filters;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using StackExchange.Profiling;

namespace BudgetOnline.Web.Controllers
{
	[HandleViewErrorsAttribute]
	public class BaseController : Controller
	{
		public IAuthProvider AuthProvier { get; set; }
		public IMembershipHelper MembershipHelper { get; set; }
		public ILogWriter LogWriter { get; set; }
		public ICacheWrapper CacheWrapper { get; set; }
		public ISessionWrapper SessionWrapper { get; set; }

		private UserModel _currentUser;
		public UserModel CurrentUser
		{
			get
			{
				return _currentUser ?? (_currentUser = MembershipHelper.GetUser());
			}
		}

		//private void PopulatedWeakReferences()
		//{
		//    var profiler = MiniProfiler.Current;
		//    using (profiler.Step("PopulatedWeakReferences"))
		//    {
		//        var properties = GetType().GetProperties();

		//        foreach (var property in properties)
		//        {
		//            if (property != null
		//                && property.PropertyType.IsInterface
		//                && (property.PropertyType.Namespace ?? string.Empty).StartsWith("BudgetOnline.")
		//                && property.GetValue(this, null) == null
		//                )
		//            {
		//                var instance = DependencyResolver.Current.GetService(property.PropertyType);
		//                if (instance != null)
		//                    property.SetValue(
		//                        this,
		//                        instance,
		//                        null);
		//            }
		//        }
		//    }
		//}
	}

	[Authorize]
    [AuthFilter]
	public class SecuredController : BaseController
	{
		public virtual bool IsSectionValid<T>(T model, Expression<Func<T, int>> sectionSelector)
			where T : class
		{
			var sectionId = MembershipHelper.CurrentUser.SectionId;

			return model != null && sectionId == sectionSelector.Compile().Invoke(model);
		}
	}

	[Authorize]
    [AuthFilter]
    public class ListController : SecuredController
	{

	}


	[Authorize]
    [AuthFilter]
    public class ListWithSearchController<TSearch> : ListController
		where TSearch : class
	{
		protected virtual string SearchCacheKey
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsSearchLastUsed
		{
			get
			{
				return CacheWrapper.Exists(SearchCacheKey);
			}
		}

		public TSearch SearchLastUsed
		{
			get
			{
				if (CacheWrapper.Exists(SearchCacheKey))
					return CacheWrapper.Get<TSearch>(SearchCacheKey);

				return Activator.CreateInstance<TSearch>();
			}
			set
			{
				CacheWrapper.Put(SearchCacheKey, value, Constants.GetDefaultSearchCacheTimeout);
			}
		}
	}
}
