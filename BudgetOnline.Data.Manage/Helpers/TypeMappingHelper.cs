using System;
using System.Data.Linq;
using AutoMapper;

namespace BudgetOnline.Data.Manage.Helpers
{
    public class TypeMappingHelper<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly MapperConfiguration _mapperConfiguration =
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Binary, byte[]>().ConvertUsing((br, bt, ctx) =>
                {
                    if (br == null)
                    {
                        return null;
                    }
                    return br.ToArray();
                });
                cfg.CreateMap<TIn, TOut>();
                cfg.CreateMap<TOut, TIn>();
            });

        public TypeMappingHelper()
        {
            Console.WriteLine("Preparing mapper in CommonRepository at {0}. In={1} Out={2}", DateTime.Now, typeof(TIn).Name, typeof(TOut).Name);
            Mapper.Initialize(cfg => cfg.CreateMap<TIn, TOut>());
        }

        public Func<TIn, TOut> OutMapper
        {
            get
            {
                var mapper = _mapperConfiguration.CreateMapper();

                return mapper.Map<TIn, TOut>;
            }
        }
        public Func<TOut, TIn> InMapper
        {
            get
            {
                var mapper = _mapperConfiguration.CreateMapper();

                return mapper.Map<TOut, TIn>;
            }
        }
    }
}
