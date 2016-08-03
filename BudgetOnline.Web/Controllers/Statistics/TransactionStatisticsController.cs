using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Web.Infrastructure.Helpers;
using BudgetOnline.Web.Models;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Controllers.Statistics
{
    public class TransactionStatisticsController : BaseController
    {
        private readonly string[] _excludeTags = new[] { "корректировка", "Верховинная" };

        public ITransactionRepository TransactionRepository { get; set; }
        public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
        public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }
        public ICurrencyRepository CurrencyRepository { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
        public IDictionaries Dictionaries { get; set; }
        public ITransactionCacheHelper TransactionCacheHelper { get; set; }
        public ISettingsHelper SettingsHelper { get; set; }
        public ICompressor Compressor { get; set; }

        public ActionResult TotalsByCurrentMonth(int? id)
        {
            int targetCurrencyId = id ?? GetDefaultCurrencyId();

            var output = TotalsByRequestedMonth(
                targetCurrencyId,
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1),
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1).AddDays(-1));

            var model = new StatisticBlockViewModel
                {
                    Title = "Статистика за текущий месяц",
                    Groups = new[] { output }
                };

            var content = HtmlHelperExtensions.RenderRazorViewToString(ControllerContext,
                                                            "~/Views/Shared/StatisticBlock.cshtml", model);

            if (Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new
                    {
                        Content = content,
                        UpdateTime = DateTimeProvider.Now().ToShortDateString() + " " + DateTimeProvider.Now().ToLongTimeString()
                    }
                };

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            return new ContentResult { Content = content };
        }

        public ActionResult TotalsByPrevMonth(int? id)
        {
            int targetCurrencyId = id ?? GetDefaultCurrencyId();

            var output = TotalsByRequestedMonth(
                targetCurrencyId,
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1),
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddDays(-1));

            var model = new StatisticBlockViewModel
            {
                Title = "Статистика за текущий месяц",
                Groups = new[] { output }
            };

            var content = HtmlHelperExtensions.RenderRazorViewToString(ControllerContext,
                                                            "~/Views/Shared/StatisticBlock.cshtml", model);

            if (Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new
                    {
                        Content = content,
                        UpdateTime = DateTimeProvider.Now().ToShortDateString() + " " + DateTimeProvider.Now().ToLongTimeString()
                    }
                };

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            return new ContentResult { Content = content };
        }

        public ActionResult LightTotalByCurrencies()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date2 = DateTime.Today
            };

            string cacheKey = string.Format("LightTotalByCurrencies_{0}", options.Date2);
            string content = TryToGetContentFromCache(cacheKey);

            if (string.IsNullOrWhiteSpace(content))
            {
                var items = TransactionStatisticsRepository.GetTotalsByCurrencies(CurrentUser.SectionId, options);
                var groups = new[]
        	                {
        	                    new StatisticBlockItemsGroupViewModel
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

                var model = new StatisticBlockViewModel
                {
                    Groups = groups,
                    Title = "Итог по валютам",
                    Tooltip = "Итоговые данные по валютам"
                };

                content = HtmlHelperExtensions.RenderRazorViewToString(ControllerContext,
                                                                "~/Views/Shared/StatisticBlock.cshtml", model);
            }

            RefreshContentInCache(cacheKey, content);

            if (Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new
                    {
                        Content = content,
                        UpdateTime = DateTimeProvider.Now().ToShortDateString() + " " + DateTimeProvider.Now().ToLongTimeString()
                    }
                };

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            return new ContentResult { Content = content };
        }

        public ActionResult CurrentBalanceByCurrencies()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date2 = DateTime.Today
            };

            string cacheKey = string.Format("CurrentBalanceByCurrencies_{0}", options.Date2);
            string content = TryToGetContentFromCache(cacheKey);

            if (string.IsNullOrWhiteSpace(content))
            {
                var items = TransactionStatisticsRepository.GetTotalsByCurrencies(CurrentUser.SectionId, options);
                var groups = new[]
                                 {
                                     new StatisticBlockItemsGroupViewModel
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

                var model = new StatisticBlockViewModel
                                {
                                    Groups = groups,
                                    Title = "Баланс по валютам",
                                    Tooltip = "Текущий баланс по валютам"
                                };

                content = HtmlHelperExtensions.RenderRazorViewToString(ControllerContext,
                                                                           "~/Views/Shared/StatisticBlock.cshtml", model);
            }

            RefreshContentInCache(cacheKey, content);

            if (Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new
                    {
                        Content = content,
                        UpdateTime = DateTimeProvider.Now().ToShortDateString() + " " + DateTimeProvider.Now().ToLongTimeString()
                    }
                };

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            return new ContentResult { Content = content };
        }

        public ActionResult LightTotalByAccounts()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date2 = DateTime.Today
            };

            string content = string.Empty;
            string cacheKey = string.Format("LightTotalByAccounts_{0}", options.Date2);

            if (SettingsHelper.GetWebSetting(CurrentUser.SectionId, null, SettingsHelperConstants.StatisticCacheActive, true))
            {
                if (CacheWrapper.Exists(cacheKey))
                {
                    var lastBalanceUpdated = TransactionCacheHelper.GetLastBalanceUpdated();
                    var cache = CacheWrapper.Get<CacheObjectWrapper>(cacheKey);
                    if (cache != null && lastBalanceUpdated <= cache.Updated)
                        content = cache.Data.ToString();
                }
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                var items = TransactionStatisticsRepository.GetTotalsByAccounts(CurrentUser.SectionId, options).ToList();

                var groups = items
                    .GroupBy(o => new { o.AccountId, o.AccountName, o.AccountIsExternal })
                    .OrderBy(o => o.Key.AccountIsExternal).ThenBy(o => o.Key.AccountName)
                    .Select(acc => new StatisticBlockItemsGroupViewModel
                                       {
                                           Id = acc.Key.AccountId.Value,
                                           Title = acc.Key.AccountName,
                                           Items = items.Where(cur => cur.AccountId == acc.Key.AccountId)
                                               .Select(cur => new StatisticDetailItemViewModel
                                                                  {
                                                                      Label = cur.CurrencyName,
                                                                      Tooltip = cur.CurrencyName,
                                                                      Sum = cur.Sum,
                                                                      CurrencySymbol = cur.CurrencySymbol
                                                                  })
                                       });

                var model = new StatisticBlockViewModel
                                {
                                    Groups = groups,
                                    Title = "Итог по счетам",
                                    Tooltip = "Итоговые данные по счетам"
                                };

                content = HtmlHelperExtensions.RenderRazorViewToString(ControllerContext,
                                                                           "~/Views/Shared/StatisticBlock.cshtml", model);
            }

            CacheWrapper.Put(cacheKey, new CacheObjectWrapper(content), new TimeSpan(0, 1, 0));

            if (Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new
                    {
                        Content = content,
                        UpdateTime = DateTimeProvider.Now().ToShortDateString() + " " + DateTimeProvider.Now().ToLongTimeString()
                    }
                };

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            return new ContentResult { Content = content };
        }

        private StatisticBlockItemsGroupViewModel TotalsByRequestedMonth(int targetCurrencyId, DateTime date1, DateTime date2)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = date1,
                Date2 = date2,
                TransactionTypes = new[] { (int)TransactionTypes.Income, (int)TransactionTypes.Expense },
                ExcludeTags = _excludeTags
            };

            var totals = TransactionRepository.GetListTotals(CurrentUser.SectionId, options).ToList();

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

            return new StatisticBlockItemsGroupViewModel
                    {
                        Items = output,
                    };
        }

        private int GetDefaultCurrencyId()
        {
            var currencies = Dictionaries.Currencies();

            var defaultCurrency = currencies.FirstOrDefault(o => o.IsDefault);
            if (defaultCurrency != null)
                return defaultCurrency.Id;

            return 0;
        }

        private string TryToGetContentFromCache(string key)
        {
            if (SettingsHelper.GetWebSetting(CurrentUser.SectionId, null, SettingsHelperConstants.StatisticCacheActive, true))
            {
                if (CacheWrapper.Exists(key))
                {
                    var lastBalanceUpdated = TransactionCacheHelper.GetLastBalanceUpdated();
                    var cache = CacheWrapper.Get<CacheObjectWrapper>(key);
                    if (cache != null && lastBalanceUpdated <= cache.Updated)
                    {
                        var content = cache.Data.ToString();
                        if (!string.IsNullOrWhiteSpace(content))
                            LogWriter.TraceFormat("Cache hit. Key: {0}", key);

                        return Compressor.Decompress(content);
                    }
                }
            }

            return null;
        }

        private void RefreshContentInCache(string key, string content)
        {
            CacheWrapper.Put(key, new CacheObjectWrapper(Compressor.Compress(content)), new TimeSpan(0, 1, 0));
        }
    }
}
