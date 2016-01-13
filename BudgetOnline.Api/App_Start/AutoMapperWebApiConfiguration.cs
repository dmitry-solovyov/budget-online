using AutoMapper;
using BudgetOnline.Data.Manage.AutomapperConfig;

namespace BudgetOnline.Api
{
    public static class AutoMapperWebApiConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DataManageProfile());
                cfg.AddProfile(new DataManageProfile());
            });
        }
    }
}