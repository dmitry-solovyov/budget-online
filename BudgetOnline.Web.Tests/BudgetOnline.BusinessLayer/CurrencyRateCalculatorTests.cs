using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.BusinessLayer.Helpers;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Currency = BudgetOnline.Data.Manage.Types.Simple.Currency;

namespace BudgetOnline.Web.Tests.BudgetOnline.BusinessLayer
{
    [TestClass]
    public class CurrencyRateCalculatorTests
    {
        [TestMethod]
        public void ConvertCurrency_SimpleExchange()
        {
            var calc = GetCurrencyRateCalculator(new[] { new Tuple<int, int, decimal, DateTime?>(1, 2, 5, null) });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 2).ToArray();

            Assert.AreEqual(2, converted[0].CurrencyId);
            Assert.AreEqual(500m, converted[0].Sum);
        }

        [TestMethod]
        public void ConvertCurrency_SimpleExchange_WhenDBContainsSeveralOptions()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(1, 3, 1, null),
                    new Tuple<int, int, decimal, DateTime?>(1, 2, 5, null)
                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 2).ToArray();

            Assert.AreEqual(2, converted[0].CurrencyId);
            Assert.AreEqual(500m, converted[0].Sum);
        }

        [TestMethod]
        public void ConvertCurrency_ReverseExchange()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(2, 1, 5, null),
                    new Tuple<int, int, decimal, DateTime?>(1, 3, 1, null)
                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 2).ToArray();

            Assert.AreEqual(2, converted[0].CurrencyId);
            Assert.AreEqual(20m, converted[0].Sum);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Exception should be raised")]
        public void ConvertCurrency_TransitExchange_WhenRateNotFound()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(1, 2, 2, null),
                    new Tuple<int, int, decimal, DateTime?>(3, 4, 2, null)
                });

            calc.ConvertCurrency(new[] { GetTransaction() }, 4);
        }


        [TestMethod]
        public void ConvertCurrency_TransitExchangeInTwoSteps()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(1, 2, 2, null),
                    new Tuple<int, int, decimal, DateTime?>(2, 3, 3, null)
                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 3).ToArray();

            Assert.AreEqual(3, converted[0].CurrencyId);
            Assert.AreEqual(600m, converted[0].Sum);
        }

        [TestMethod]
        public void ConvertCurrency_TransitExchangeInTwoSteps_WhenBothReverse()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(2, 1, 2m, null),
                    new Tuple<int, int, decimal, DateTime?>(3, 1, 3m, null)
                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction(3) }, 2).ToArray();

            Assert.AreEqual(2, converted[0].CurrencyId);
            Assert.AreEqual(150m, converted[0].Sum);
        }

        [TestMethod]
        public void ConvertCurrency_TransitExchangeInFourSteps()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(1, 2, 2m, null),
                    new Tuple<int, int, decimal, DateTime?>(2, 3, 3m, null),
                    new Tuple<int, int, decimal, DateTime?>(3, 4, 5m, null),
                    new Tuple<int, int, decimal, DateTime?>(4, 5, 7m, null),

                    new Tuple<int, int, decimal, DateTime?>(3, 6, 1.5m, null),
                    new Tuple<int, int, decimal, DateTime?>(1, 6, 3.5m, null)

                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 5).ToArray();

            Assert.AreEqual(5, converted[0].CurrencyId);
            Assert.AreEqual(21000m, converted[0].Sum);
        }


        [TestMethod]
        public void ConvertCurrency_TransitExchangeInFourSteps_WhenOneRateIsReverse()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(1, 2, 2m, null),
                    new Tuple<int, int, decimal, DateTime?>(2, 3, 3m, null),
                    new Tuple<int, int, decimal, DateTime?>(4, 3, 1m/5, null),
                    new Tuple<int, int, decimal, DateTime?>(4, 5, 7m, null),

                    new Tuple<int, int, decimal, DateTime?>(3, 6, 1.5m, null),
                    new Tuple<int, int, decimal, DateTime?>(1, 6, 3.5m, null)

                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 5).ToArray();

            Assert.AreEqual(5, converted[0].CurrencyId);
            Assert.AreEqual(21000m, converted[0].Sum);
        }

        [TestMethod]
        public void ConvertCurrency_TransitExchangeInFourSteps_WhenCycle()
        {
            var calc = GetCurrencyRateCalculator(
                new[] 
                { 
                    new Tuple<int, int, decimal, DateTime?>(1, 2, 2m, null),
                    new Tuple<int, int, decimal, DateTime?>(2, 3, 3m, null),
                    new Tuple<int, int, decimal, DateTime?>(3, 2, 1m/3, null),
                    new Tuple<int, int, decimal, DateTime?>(3, 5, 7m, null),

                    new Tuple<int, int, decimal, DateTime?>(3, 6, 1.5m, null),
                    new Tuple<int, int, decimal, DateTime?>(1, 6, 3.5m, null)

                });
            var converted = calc.ConvertCurrency(new[] { GetTransaction() }, 5).ToArray();

            Assert.AreEqual(5, converted[0].CurrencyId);
            Assert.AreEqual(4200m, converted[0].Sum);
        }


        private Transaction GetTransaction(int currencyId = 1)
        {
            return new Transaction
                       {
                           CurrencyId = currencyId,
                           Sum = 100m,
                           Date = DateTime.Today,
                       };
        }

        private CurrencyRateCalculator GetCurrencyRateCalculator(params Tuple<int, int, decimal, DateTime?>[] currencyPairs)
        {
            return new CurrencyRateCalculator
                       {
                           CurrencyRateRepository = GetCurrencyRateRepositoryMock(currencyPairs).Object,
                           DateTimeProvider = GetDateTimeProviderMock().Object,
                           Dictionaries = GetDictionariesMock().Object,
                           CurrentUserProvider = GetCurrentUserProviderMock().Object
                       };
        }

        private Mock<ICurrencyRateRepository> GetCurrencyRateRepositoryMock(params Tuple<int, int, decimal, DateTime?>[] currencyPairs)
        {
            var mock = new Mock<ICurrencyRateRepository>();
            mock.Setup(o => o.GetLastRates(It.IsAny<int>()))
                .Returns(BuildRates(currencyPairs));

            return mock;
        }

        private readonly Currency[] _currencies = new[]
                                             {
                                                 new Currency{Id = 1, Name = "Cr 1", IsDisabled = false, IsDefault = true, SectionId = 1},
                                                 new Currency{Id = 2, Name = "Cr 2", IsDisabled = false, IsDefault = false, SectionId = 1},
                                                 new Currency{Id = 3, Name = "Cr 3", IsDisabled = false, IsDefault = false, SectionId = 1},
                                                 new Currency{Id = 4, Name = "Cr 4", IsDisabled = false, IsDefault = false, SectionId = 1},
                                                 new Currency{Id = 5, Name = "Cr 5", IsDisabled = false, IsDefault = false, SectionId = 1},
                                                 new Currency{Id = 6, Name = "Cr 6", IsDisabled = false, IsDefault = false, SectionId = 1}
                                             };

        private IEnumerable<CurrencyRate> BuildRates(params Tuple<int, int, decimal, DateTime?>[] currencyPairs)
        {
            int id = 1;

            return currencyPairs
                .Select(currencyTuple =>
                    new CurrencyRate
                    {
                        Id = id++,
                        Date = currencyTuple.Item4 ?? DateTime.Today.AddYears(-1),
                        SectionId = 1,
                        Rate = currencyTuple.Item3,
                        BaseCurrencyId = currencyTuple.Item1,
                        TargetCurrencyId = currencyTuple.Item2
                    }).ToList();
        }

        private Mock<IDictionaries> GetDictionariesMock()
        {
            var mock = new Mock<IDictionaries>();
            mock.Setup(o => o.Currencies()).Returns(_currencies);
            return mock;
        }

        private Mock<IDateTimeProvider> GetDateTimeProviderMock()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(o => o.Now(It.IsAny<DateTimeKind>())).Returns(DateTime.Now);
            return mock;
        }

        private Mock<ICurrentUserProvider> GetCurrentUserProviderMock()
        {
            var mock = new Mock<ICurrentUserProvider>();
            mock.Setup(o => o.SectionId).Returns(1);

            return mock;
        }

    }
}
