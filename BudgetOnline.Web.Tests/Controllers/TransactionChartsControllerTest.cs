using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Common.Helpers;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Controllers.Statistics;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Controllers
{
	[TestClass]
	public class TransactionChartsControllerTest
	{
		private readonly Mock<MembershipHelper> _membershipHelper = new Mock<MembershipHelper>();

		private readonly UserModel _currentUser =
			new UserModel
			{
				Id = 1,
				SectionId = 1,
			};

		[TestInitialize]
		public void Setup()
		{
			_membershipHelper
				.Setup(o => o.GetUser())
				.Returns(_currentUser);

			_membershipHelper
				.Setup(o => o.CurrentUser)
				.Returns(_currentUser);
		}


		[TestMethod]
		public void CategoriesByMonth_ShouldReturnModel_WhenChartActionCalled()
		{
			var controller = GetController();

			var result = controller.CategoriesByPeriodPercents();

			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Highcharts));
		}


		//[TestMethod]
		//public void CategoriesByMonth_Should_When()
		//{
		//    var controller = GetController();

		//    var result = controller.CategoriesByMonth();

		//    var model = ((ViewResult)result).Model as Highcharts;
		//}

		private TransactionChartsController GetController()
		{
			var controller = new TransactionChartsController
			                 	{
			                 		MembershipHelper = _membershipHelper.Object,
									TransactionStatisticsRepository = GetTransactionStatisticsRepositoryMock().Object,
									DateTimeProvider = GetDateTimeProviderMock().Object,
									DateTimeHelpers = GetDateTimeHelperMock().Object,
			                 	};
			
			return controller;
		}

		private Mock<IDateTimeProvider> GetDateTimeProviderMock()
		{
			var m = new Mock<IDateTimeProvider>();
			m.Setup(o => o.Now(DateTimeKind.Local)).Returns(DateTime.Now);
            m.Setup(o => o.Now(DateTimeKind.Utc)).Returns(DateTime.UtcNow);
			return m;
		}


		private Mock<IDateTimeHelper> GetDateTimeHelperMock()
		{
			var m = new Mock<IDateTimeHelper>();
			m.Setup(o => o.IsEqualDates(It.IsAny<TimePeriodTypes>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns((DateTime d1, DateTime d2) => d1.Month.Equals(d2.Month) && d1.Year.Equals(d2.Year));


			m.Setup(o => o.SequenceOfDates(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.Is<TimePeriodTypes>(t => t == TimePeriodTypes.Monthly)))
				.Returns(GetDates().ToArray());

			return m;
		}

		private Mock<ITransactionStatisticsRepository> GetTransactionStatisticsRepositoryMock(IEnumerable<TransactionTotal> totals = null, IEnumerable<TransactionTotal> statistics = null)
		{
			var m = new Mock<ITransactionStatisticsRepository>();
			m.Setup(o => o.GetTotalsByCategories(It.IsAny<int>(), It.IsAny<TransactionStatisticsSearchOptions>()))
				.Returns(totals ?? GetTotals());
			m.Setup(o => o.GetStatistictsByCategory(It.IsAny<int>(), It.IsAny<TransactionStatisticsSearchOptions>()))
				.Returns(statistics ?? GetStatistics());

			return m;
		}

		private IEnumerable<DateTime> GetDates()
		{
			var firstDate = new DateTime(DateTime.Now.Year, 1, 1);
			return Enumerable.Range(0, 10).Select(firstDate.AddMonths);
		}

		private IEnumerable<TransactionTotal> GetTotals()
		{
			yield return new TransactionTotal
						{
							CategoryId = 1,
							CategoryName = "Cat1",
							Sum = -100m
						};
			yield return new TransactionTotal
			{
				CategoryId = 2,
				CategoryName = "Cat2",
				Sum = -77m
			};
		}

		private IEnumerable<TransactionTotal> GetStatistics()
		{
			var dates = GetDates().ToList();
			yield return new TransactionTotal
			{
				CategoryId = 1,
				CategoryName = "Cat1",
				Date = dates.First(),
				Sum = -10m
			};
			yield return new TransactionTotal
			{
				CategoryId = 1,
				CategoryName = "Cat1",
				Date = dates.Skip(1).First(),
				Sum = -20m
			};
			yield return new TransactionTotal
			{
				CategoryId = 1,
				CategoryName = "Cat1",
				Date = dates.Skip(2).First(),
				Sum = -30m
			};
			yield return new TransactionTotal
			{
				CategoryId = 1,
				CategoryName = "Cat1",
				Date = dates.Skip(3).First(),
				Sum = -40m
			};


			yield return new TransactionTotal
			{
				CategoryId = 2,
				CategoryName = "Cat2",
				Date = dates.First(),
				Sum = -15m
			};
			yield return new TransactionTotal
			{
				CategoryId = 2,
				CategoryName = "Cat2",
				Date = dates.Skip(1).First(),
				Sum = -25m
			};
			yield return new TransactionTotal
			{
				CategoryId = 2,
				CategoryName = "Cat2",
				Date = dates.Skip(3).First(),
				Sum = -33m
			};
		}
	}
}
