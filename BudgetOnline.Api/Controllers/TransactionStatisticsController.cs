using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.ViewModels;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using Newtonsoft.Json;

namespace BudgetOnline.Api.Controllers
{
    [RoutePrefix("transactions/statistics")]
    public class TransactionStatisticsController : BaseApiAuthController
    {
        private readonly string[] _excludeTags = { "корректировка", "Верховинная" };

        public ITransactionRepository TransactionRepository { get; set; }
        public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
        public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }
        public ICurrencyRepository CurrencyRepository { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
        public IDictionaries Dictionaries { get; set; }

        [HttpGet]
        [Route("TotalsByCurrentMonth")]
        public HttpResponseMessage TotalsByCurrentMonth()
        {
            return TotalsByCurrentMonth(0);
        }

        [HttpGet]
        [Route("TotalsByCurrentMonth/{id}")]
        public HttpResponseMessage TotalsByCurrentMonth(int id)
        {
            var targetCurrencyId = id > 0 ? id : GetDefaultCurrencyId();

            var output = TotalsByRequestedMonth(
                targetCurrencyId,
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1),
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1).AddDays(-1));

            return PrepareResponse(output);
        }

        [HttpGet]
        [Route("TotalsByPrevMonth/{id}")]
        public HttpResponseMessage TotalsByPrevMonth(int id)
        {
            int targetCurrencyId = id > 0 ? id : GetDefaultCurrencyId();

            var output = TotalsByRequestedMonth(
                targetCurrencyId,
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1),
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddDays(-1));

            return PrepareResponse(output);
        }

        [HttpGet]
        [Route("TotalByCurrencies")]
        public HttpResponseMessage TotalByCurrencies()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date2 = DateTime.Today
            };

            var items = TransactionStatisticsRepository.GetTotalsByCurrencies(CurrentApiUserProvider.CurrentSession.User.SectionId, options);
            var groups = new[]
        	                {
        	                    new StatisticItemsGroupViewModel
        	                     	{
        	                     		Title = "",
        	                     		Items = items.Select(cur => new StatisticDetailItemViewModel
        	                     				                    {
        	                     				                        Label = cur.CurrencyName,
        	                     				                        Sum = cur.Sum,
																		CurrencySymbol = cur.CurrencySymbol
        	                     				                    }),
        	                     	}
        	                };

            return PrepareResponse(groups);
        }

        [HttpGet]
        [Route("TotalByAccounts")]
        public HttpResponseMessage TotalByAccounts()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date2 = DateTime.Today
            };

            var items = TransactionStatisticsRepository.GetTotalsByAccounts(CurrentApiUserProvider.CurrentSession.User.SectionId, options).ToList();

            var groups = items
                .GroupBy(o => new { o.AccountId, o.AccountName, o.AccountIsExternal })
                .OrderBy(o => o.Key.AccountIsExternal).ThenBy(o => o.Key.AccountName)
                .Select(acc => new StatisticItemsGroupViewModel
                                   {
                                       Title = acc.Key.AccountName,
                                       Items = items.Where(cur => cur.AccountId == acc.Key.AccountId)
                                           .Select(cur => new StatisticDetailItemViewModel
                                                              {
                                                                  Label = cur.CurrencyName,
                                                                  Sum = cur.Sum,
                                                                  CurrencySymbol = cur.CurrencySymbol
                                                              })
                                   });

            return PrepareResponse(groups);
        }

        private StatisticDetailItemViewModel[] TotalsByRequestedMonth(int targetCurrencyId, DateTime date1, DateTime date2)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = date1,
                Date2 = date2,
                TransactionTypes = new[] { (int)TransactionTypes.Income, (int)TransactionTypes.Expense },
                ExcludeTags = _excludeTags
            };

            var totals = TransactionRepository.GetListTotals(CurrentApiUserProvider.CurrentSession.User.SectionId, options).ToList();

            string selectedCurrencyName = string.Empty;
            var selectedCurrency = CurrencyRepository.Get(targetCurrencyId);
            if (selectedCurrency != null)
                selectedCurrencyName = selectedCurrency.Name;


            var totalsPositiveSum = CurrencyRateCalculator.ConvertCurrency(totals.Where(o => !o.SumNegative), targetCurrencyId).Sum(o => o.Sum);

            var totalsNegativeSum = CurrencyRateCalculator.ConvertCurrency(totals.Where(o => o.SumNegative), targetCurrencyId).Sum(o => o.Sum);

            var output = new[] {
					new StatisticDetailItemViewModel { Label = "Доходы",  Sum= totalsPositiveSum,  CurrencySymbol = selectedCurrencyName},
					new StatisticDetailItemViewModel { Label = "Расходы", Sum = totalsNegativeSum, CurrencySymbol = selectedCurrencyName},
					new StatisticDetailItemViewModel { Label = "Итог", Sum = totalsPositiveSum+totalsNegativeSum, CurrencySymbol = selectedCurrencyName}
			};

            return output;
        }

        private int GetDefaultCurrencyId()
        {
            var currencies = Dictionaries.Currencies();

            var defaultCurrency = currencies.FirstOrDefault(o => o.IsDefault);
            if (defaultCurrency != null)
                return defaultCurrency.Id;

            return 0;
        }
    }
}
