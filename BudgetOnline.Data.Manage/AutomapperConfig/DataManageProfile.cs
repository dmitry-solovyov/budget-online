using System;
using AutoMapper;

namespace BudgetOnline.Data.Manage.AutomapperConfig
{
    public class DataManageProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Types.Simple.UserConnect, MSSQL.UserConnect>()
                .ForMember(m => m.ExpiresWhen, x => x.MapFrom(m => m.ExpiresWhen.AsLocal()))
                .ForMember(m => m.CreatedWhen, x => x.MapFrom(m => m.CreatedWhen.AsLocal()));

            Mapper.CreateMap<MSSQL.UserConnect, Types.Simple.UserConnect>()
                .ForMember(m => m.ExpiresWhen, x => x.MapFrom(m => m.ExpiresWhen.AsUtc()))
                .ForMember(m => m.CreatedWhen, x => x.MapFrom(m => m.CreatedWhen.AsUtc()));
        }
    }
}
