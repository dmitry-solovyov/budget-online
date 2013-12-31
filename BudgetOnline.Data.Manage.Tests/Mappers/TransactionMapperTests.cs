using System;
using AutoMapper;
using TransactionDb = BudgetOnline.Data.MSSQL.Transaction;
using TransactionSimple = BudgetOnline.Data.Manage.Types.Simple.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.Data.Manage.Tests.Mappers
{
	[TestClass]
	public class TransactionMapperTests
	{
		[TestMethod]
		public void SimpleToDbMapper()
		{
			Mapper.CreateMap<TransactionSimple, TransactionDb>();
			//Mapper.AssertConfigurationIsValid();

			var source = new TransactionSimple
			{
				Date = DateTime.Now.Date,
				CreatedWhen = DateTime.Now,
				Sum = 100m,
			};

			TransactionDb result = Mapper.Map<TransactionSimple, TransactionDb>(source);

			Assert.AreEqual(source.Date, result.Date);
			Assert.AreEqual(source.CreatedWhen, result.CreatedWhen);
		}
	}
}
