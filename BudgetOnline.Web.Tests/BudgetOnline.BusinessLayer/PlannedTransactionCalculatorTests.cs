using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.BusinessLayer.Helpers;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.BudgetOnline.BusinessLayer
{
	[TestClass]
	public class PlannedTransactionCalculatorTests
	{
		private readonly Mock<IPlannedTransactionRepository> _plannedTransactionRepositoryMock = new Mock<IPlannedTransactionRepository>();
		private readonly Mock<ITransactionStatisticsRepository> _transactionStatisticsRepositoryMock = new Mock<ITransactionStatisticsRepository>();
		private readonly Mock<ICurrencyRateCalculator> _currencyRateCalculatorMock = new Mock<ICurrencyRateCalculator>();
		private readonly Mock<IPeriodTypeRepository> _periodTypeRepositoryMock = new Mock<IPeriodTypeRepository>();
		private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new Mock<IDateTimeProvider>();

		private enum PeriodTypes
		{
			Dayly = 1,
			Monthly,
			Anually,
			Weekly,
			Once
		}

		private IEnumerable<Currency> GetCurrencies()
		{
			return new[]
				{
					new Currency {Id = 1, Name = "Hrn"},
					new Currency {Id = 2, Name = "Usd"},
				};
		}

		private CurrentCurrencyRate GetCurrentCurrencyRate()
		{
			return new CurrentCurrencyRate { CurrencyId = 1, CurrencyName = "A", Rate = 0.5m, Date = DateTime.Today };
		}

		private TransactionTotal[] GetTtransactionActualTotals()
		{
			return new[]
				{
					new TransactionTotal {CurrencyId = 1, Sum = 100m, Date = DateTime.Today},
					new TransactionTotal {CurrencyId = 2, Sum = 12m, Date = DateTime.Today}
				};
		}


		[TestInitialize]
		public void Setup()
		{
			SetupPeriodTypesRepositoryMock();
			SetupCurrencyRateCalculatorMock();
			SetupTransactionStatisticsRepositoryMock();
			SetupDateTimeProviderMock();
		}


		private PlannedTransactionCalculator GetCalculator()
		{
			return new PlannedTransactionCalculator
					{
						PeriodTypeRepository = _periodTypeRepositoryMock.Object,
						PlannedTransactionRepository = _plannedTransactionRepositoryMock.Object,
						TransactionStatisticsRepository = _transactionStatisticsRepositoryMock.Object,
						DateTimeProvider = _dateTimeProviderMock.Object
					};
		}

		[TestMethod]
		public void Calculate_ShouldExcludeRecords_WhenTheyAreNotInPeriod()
		{
			SetupPlannedTransactionRepositoryMock();

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2012, 1, 1), Date2 = new DateTime(2013, 1, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(0, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldExcludeRecords_WhenTheyAreDisabled()
		{
			SetupPlannedTransactionRepositoryMock(disabled: true);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2014, 1, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(0, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldIncludeRecords_WhenTheyAreInPeriod()
		{
			SetupPlannedTransactionRepositoryMock();

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2014, 1, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(1, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPlannedRecordHasActivPeriodLessThenReportPeriod()
		{
			SetupPlannedTransactionRepositoryMock(PeriodTypes.Monthly);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2014, 1, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(5, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPlannedRecordHasActivPeriodGraterThenReportPeriod()
		{
			SetupPlannedTransactionRepositoryMock(PeriodTypes.Monthly);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2013, 8, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(2, result.Count);
		}


		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPlannedRecordStartsBeforeReportPeriod()
		{
			SetupPlannedTransactionRepositoryMock(PeriodTypes.Monthly);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 10, 1), Date2 = new DateTime(2013, 12, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(2, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPlannedRecordStartsBeforeReportPeriodAnually()
		{
			SetupPlannedTransactionRepositoryMock();

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 7, 1), Date2 = new DateTime(2013, 12, 1) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(1, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPlannedRecordStartsAndStopsInsideReportPeriod()
		{
			SetupPlannedTransactionRepositoryMock(PeriodTypes.Monthly);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 8, 1), Date2 = new DateTime(2013, 8, 20) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(1, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenMultiplePlannedRecordProcessed()
		{
			SetupPlannedTransactionRepositoryMockMany();

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2013, 12, 31) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(5, result.Count);
		}

		[TestMethod]
		public void Calculate_ShouldCorrectlyCalculateSum_WhenGroupedByMonths()
		{
			SetupPlannedTransactionRepositoryMockMany();

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2013, 12, 31) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(100m, result[0].Sum);
			Assert.AreEqual(133m, result[1].Sum);
			Assert.AreEqual(133m, result[2].Sum);
			Assert.AreEqual(133m, result[3].Sum);
			Assert.AreEqual(100m, result[4].Sum);
		}

		[TestMethod]
		public void FindPlansRelatedToDate_ShouldRturnCorrectRows_WhenSearchPerformed()
		{
			SetupPlannedTransactionRepositoryMockMany();

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 10, 1), Date2 = new DateTime(2013, 12, 31) };

			var result = calculator.FindPlansRelatedToDate(1, new DateTime(2013, 10, 1), new DateTime(2013, 12, 31)).ToList();

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(1, result[0].Id, "Id");
		}


		[TestMethod]
		public void FindPlansRelatedToDate_ShouldRturnEmptyRowSet_WhenSearchPerformed()
		{
			SetupPlannedTransactionRepositoryMockMany(toMonth: null);

			var calculator = GetCalculator();

			var result = calculator.FindPlansRelatedToDate(1, new DateTime(2013, 12, 1), new DateTime(2013, 12, 31)).ToList();

			Assert.AreEqual(2, result.Count);
			Assert.AreEqual(1, result[0].Id, "Id");
		}

		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPeriodTypeSetOnce()
		{
			SetupPlannedTransactionRepositoryMock(PeriodTypes.Once);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2015, 12, 31) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(1, result.Count);
		}


		[TestMethod]
		public void Calculate_ShouldGenerateCorrectRows_WhenPlannedRecordStartsBeforeReportPeriodWithoutFinishDate()
		{
			SetupPlannedTransactionRepositoryMock(PeriodTypes.Monthly, 1, 1, null);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 8, 1), Date2 = new DateTime(2013, 12, 31) };

			var result = calculator.GenerateActual(1, options, null, true).ToList();

			Assert.AreEqual(5, result.Count);
		}


		[TestMethod]
		public void Calculate_ShouldCalculateSumInOneCurrency_WhenMultipleCurrenciesUsed()
		{
			SetupPlannedTransactionRepositoryMockMany(secondCurrencyId: 2);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2013, 12, 31) };

			var result = calculator.GenerateActual(1, options, 1, true).ToList();

			Assert.AreEqual(5, result.Count);
			Assert.AreEqual(100m, result[0].Sum);
			Assert.AreEqual(166m, result[1].Sum);
			Assert.AreEqual(166m, result[2].Sum);
			Assert.AreEqual(166m, result[3].Sum);
			Assert.AreEqual(100m, result[4].Sum);
		}

		[TestMethod]
		public void Calculate_ShouldGenerateActualTransactions_WhenGenerateByOnePlannedTransaction()
		{
			SetupPlannedTransactionRepositoryMockMany(secondCurrencyId: 2);

			var calculator = GetCalculator();

			var result = calculator.GenerateActualByPlannedTransaction(1, new DateTime(2013, 1, 1), new DateTime(2013, 12, 31), 1).ToList();

			Assert.AreEqual(5, result.Count);
			Assert.AreEqual(100m, result[0].Sum);
			Assert.AreEqual(100m, result[1].Sum);
			Assert.AreEqual(100m, result[2].Sum);
			Assert.AreEqual(100m, result[3].Sum);
			Assert.AreEqual(100m, result[4].Sum);

			Assert.IsTrue(result.All(o => o.CurrencyId == 1));
		}

		[TestMethod]
		public void Calculate_ShouldReturnRowsInOneCurrency_WhenMultipleCurrenciesUsed()
		{
			SetupPlannedTransactionRepositoryMockMany(secondCurrencyId: 2);

			var calculator = GetCalculator();

			var result = calculator.GenerateActualByPlannedTransaction(1, new DateTime(2013, 1, 1), new DateTime(2013, 12, 31), 1).ToList();

			Assert.AreEqual(5, result.Count);
			Assert.AreEqual(1, result[0].CurrencyId);
			Assert.AreEqual(1, result[1].CurrencyId);
			Assert.AreEqual(1, result[2].CurrencyId);
			Assert.AreEqual(1, result[3].CurrencyId);
			Assert.AreEqual(1, result[4].CurrencyId);
		}

		[TestMethod]
		public void Calculate_ShouldCalculateSumInOneCurrency_WhenMultipleCurrenciesUsedAndReverseRateAssigned()
		{
			SetupPlannedTransactionRepositoryMockMany(secondCurrencyId: 2);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(2013, 1, 1), Date2 = new DateTime(2013, 12, 31) };

			var result = calculator.GenerateActual(1, options, 2, true).ToList();

			Assert.AreEqual(5, result.Count);
			Assert.AreEqual(50m, result[0].Sum);
			Assert.AreEqual(83m, result[1].Sum);
			Assert.AreEqual(83m, result[2].Sum);
			Assert.AreEqual(83m, result[3].Sum);
			Assert.AreEqual(50m, result[4].Sum);
		}

		[TestMethod]
		public void Calculate_ShouldExcludeRecords_WhenStartDatePassedInCurrentMonth()
		{
			var t = new PlannedTransaction
				{
					CurrencyId = 1,
					Sum = 100m,
					Amount = 1m,
					PeriodTypeId = (int)PeriodTypes.Monthly,
					FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
					ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(10),
				};

			SetupPlannedTransactionRepositoryMockFromCunsumer(t);

			var calculator = GetCalculator();

			var options = new PlannedTransactionSearchOptions { Date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20), Date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20).AddMonths(1) };

			var result = calculator.GenerateActual(1, options, 1, true).ToList();

			Assert.IsTrue(result.Count > 0);
			Assert.IsTrue(result[0].Date.Value.Month > DateTime.Now.Month);
		}

		private void SetupPeriodTypesRepositoryMock()
		{
			var result = new[] {
			             		new PeriodType {
										Id = (int)PeriodTypes.Dayly,
										Name = "Dayly",
										Days = 1,
			             			},
			             		new PeriodType {
										Id = (int)PeriodTypes.Monthly,
										Name = "Monthly",
										Monthes = 1,
			             			},
			             		new PeriodType {
										Id = (int)PeriodTypes.Anually,
										Name = "Anually",
										Years = 1,
			            			},
			             		new PeriodType {
										Id = (int)PeriodTypes.Weekly,
										Name = "Weekly",
										Weeks = 1,
			             			},
			             		new PeriodType {
										Id = (int)PeriodTypes.Once,
										Name = "Once",
			             			},		
			};

			_periodTypeRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(result);
		}

		private void SetupCurrencyRateCalculatorMock(bool reverers = false)
		{
			_currencyRateCalculatorMock
				.Setup(o => o.GetRate(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(GetCurrentCurrencyRate());
		}

		private void SetupTransactionStatisticsRepositoryMock(bool emptyTotals = true)
		{
			_transactionStatisticsRepositoryMock
				.Setup(o => o.GetTotalsByCurrencies(It.IsAny<int>(), It.IsAny<TransactionStatisticsSearchOptions>()))
				.Returns(emptyTotals ? new TransactionTotal[0] : GetTtransactionActualTotals());
		}

		private void SetupDateTimeProviderMock()
		{
			_dateTimeProviderMock.Setup(o => o.Now(DateTimeKind.Local)).Returns(DateTime.Now);
            _dateTimeProviderMock.Setup(o => o.Now(DateTimeKind.Utc)).Returns(DateTime.UtcNow);
		}

		private void SetupPlannedTransactionRepositoryMock(PeriodTypes period = PeriodTypes.Anually, int day = 1, int fromMonth = 7, int? toMonth = 11, int year = 2013, bool disabled = false)
		{
			SetupPlannedTransactionRepositoryMockFromCunsumer(
				new PlannedTransaction
				{
					CurrencyId = 1,
					Sum = 100m,
					Amount = 1m,
					PeriodTypeId = (int)period,
					FromDate = new DateTime(year, fromMonth, day),
					ToDate =
						toMonth.HasValue
							? new DateTime(year, toMonth.Value, day)
							: (DateTime?)null,
					IsDisabled = disabled
				});
		}

		private void SetupPlannedTransactionRepositoryMockFromCunsumer(PlannedTransaction transaction)
		{
			_plannedTransactionRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>(), It.IsAny<PlannedTransactionSearchOptions>()))
				.Returns(new[] { transaction });

			_plannedTransactionRepositoryMock
				.Setup(o => o.GetById(It.IsAny<int>()))
				.Returns<int>(id => new[] { transaction }.FirstOrDefault(o => o.Id == id));
		}

		private void SetupPlannedTransactionRepositoryMockMany(PeriodTypes period = PeriodTypes.Monthly, int fromMonth = 6, int? toMonth = 10, int year = 2013, bool disabled = false, int secondCurrencyId = 1)
		{
			var result = new[]
			             	{
			             		new PlannedTransaction
			             			{
										Id = 1,
			             				CurrencyId = 1,
										Sum = 100m,
										Amount = 1m,
										PeriodTypeId = (int)period,
										FromDate = new DateTime(year, fromMonth, 1),
										ToDate = toMonth.HasValue ? new DateTime(year, toMonth.Value, 1) : (DateTime?)null,
										IsDisabled = false
			             			},
			             		new PlannedTransaction
			             			{
										Id = 2,
			             				CurrencyId = secondCurrencyId,
										Sum = 33m,
										Amount = 1m,
										PeriodTypeId = (int)period,
										FromDate = new DateTime(year, fromMonth + 1, 20),
										ToDate = toMonth.HasValue ? new DateTime(year, toMonth.Value-1, 20) : (DateTime?)null,
										IsDisabled = disabled
			             			},
			             	};

			_plannedTransactionRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>(), It.IsAny<PlannedTransactionSearchOptions>()))
				.Returns(result);


			_plannedTransactionRepositoryMock
				.Setup(o => o.GetById(It.IsAny<int>()))
				.Returns<int>(id => result.FirstOrDefault(o => o.Id == id));
		}
	}
}
