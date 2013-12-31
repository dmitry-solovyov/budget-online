using System;
using AutoMapper;

namespace BudgetOnline.Data.Manage.Helpers
{
	public class TypeMappingHelper<TIn, TOut>
		where TIn : class
		where TOut : class
	{
		public TypeMappingHelper()
		{
			Console.WriteLine(string.Format("Preparing mapper in CommonRepository at {0}. In={1} Out={2}", DateTime.Now, typeof(TIn).Name, typeof(TOut).Name));
			Mapper.CreateMap<TIn, TOut>();
		}

		public readonly Func<TIn, TOut> OutMapper =
			source => Mapper.DynamicMap<TIn, TOut>(source);

		public readonly Func<TOut, TIn> InMapper =
			source => Mapper.DynamicMap<TOut, TIn>(source);
	}
}
