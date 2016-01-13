using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Infrastructure
{
    public class Dictionaries : IDictionaries
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }

        private int _section = -1;
        private int SectionId
        {
            get
            {
                if (_section < 0)
                    _section = CurrentApiUserProvider.CurrentSession.User.SectionId;

                return _section;
            }
        }

        private IEnumerable<TEntity> GenericExtractor<TEntity, TRepo>(string name, Func<TRepo, IEnumerable<TEntity>> repositoryGetter)
        {
            using (var scope = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.BeginScope())
            {
                var repository = (TRepo)scope.GetService(typeof(TRepo));

                return repositoryGetter(repository);
            }
        }

        public IEnumerable<Account> Accounts()
        {
            return GenericExtractor<Account, IAccountRepository>("account", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
        }

        public void ResetAccounts()
        {
        }

        public IEnumerable<Currency> Currencies()
        {
            return GenericExtractor<Currency, ICurrencyRepository>("currency", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
        }
        public void ResetCurrencies()
        {
        }

        public IEnumerable<Category> Categories()
        {
            return GenericExtractor<Category, ICategoryRepository>("category", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
        }
        public void ResetCategories()
        {
        }

        public IEnumerable<PeriodType> PeriodTypes()
        {
            return GenericExtractor<PeriodType, IPeriodTypeRepository>("periodType", r => r.GetList(SectionId).Where(o => !o.IsDisabled).ToList());
        }
        public void ResetPeriodTypes()
        {
        }
    }
}