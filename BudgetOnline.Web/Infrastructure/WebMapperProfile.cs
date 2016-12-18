using AutoMapper;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Areas.Admin.Models;

namespace BudgetOnline.Web.Infrastructure
{
    public class WebMapperProfile
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountEditViewModel>();
                cfg.CreateMap<AccountEditViewModel, Account>();
                cfg.CreateMap<PeriodType, PeriodTypeEditViewModel>();
                cfg.CreateMap<PeriodTypeEditViewModel, PeriodType>();
                cfg.CreateMap<Currency, CurrencyEditViewModel>();
                cfg.CreateMap<CurrencyEditViewModel, Currency>();
                cfg.CreateMap<CurrencyRate, CurrencyRateEditViewModel>();
                cfg.CreateMap<CurrencyRateEditViewModel, CurrencyRate>();
                cfg.CreateMap<TagEditViewModel, Tag>();
                cfg.CreateMap<AccountEditViewModel, Account>();
                cfg.CreateMap<Account, AccountEditViewModel>();
                cfg.CreateMap<CategoryEditViewModel, Category>();
                cfg.CreateMap<Category, CategoryEditViewModel>();
            });

            return config;
        }
    }
}