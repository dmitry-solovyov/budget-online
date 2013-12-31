using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Infrastructure.Security;

namespace BudgetOnline.Web.Infrastructure.Core
{
	public class CachedDictionaries : ICachedDictionaries
	{
		public ICachedStorage CachedStorageImpl = DependencyResolver.Current.GetService<ICachedStorage>();
		public IMembershipHelper MembershipHelperImpl = DependencyResolver.Current.GetService<IMembershipHelper>();

		private readonly TimeSpan _defaultExpirationDelay = new TimeSpan(0, 0, 10);

		private int _section = -1;
		private int SectionId
		{
			get
			{
				if (_section < 0)
					_section = MembershipHelperImpl.CurrentUser.SectionId;

				return _section;
			}
		}

		private IEnumerable<TEntity> GenericExtractor<TEntity, TRepo>(string name, Func<TRepo, IEnumerable<TEntity>> repositoryGetter)
		{
			string cachedKey = string.Format("list_of_{1}_{0}", SectionId, name);

			var repository = (TRepo)DependencyResolver.Current.GetService(typeof(TRepo));

			if (!AppSettings.TurnOffCacheForDictionaries)
				return CachedStorageImpl.GetCahedObject(
					cachedKey,
					_defaultExpirationDelay,
					() => repositoryGetter(repository));

			return repositoryGetter(repository);
		}

		private void ResetList(string name)
		{
			string cachedKey = string.Format("list_of_{1}_{0}", SectionId, name);

			CachedStorageImpl.RemoveObject(cachedKey);
		}

		public IEnumerable<Account> Accounts()
		{
			return GenericExtractor<Account, IAccountRepository>("account", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
		}

		public void  ResetAccounts()
		{
			ResetList("account");
		}

		public IEnumerable<Currency> Currencies()
		{
			return GenericExtractor<Currency, ICurrencyRepository>("currency", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
		}
		public void ResetCurrencies()
		{
			ResetList("currency");
		}

		public IEnumerable<Category> Categories()
		{
			return GenericExtractor<Category, ICategoryRepository>("category", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
		}
		public void ResetCategories()
		{
			ResetList("category");
		}

		public IEnumerable<PeriodType> PeriodTypes()
		{
			return GenericExtractor<PeriodType, IPeriodTypeRepository>("periodType", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
		}
		public void ResetPeriodTypes()
		{
			ResetList("periodType");
		}
	}
}