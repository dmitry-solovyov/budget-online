﻿using AutoMapper;

namespace BudgetOnline.Api.Admin.Infrastructure
{
    public static class AutoMapperWebApiConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile(new DataManageProfile());
            });
        }
    }
}