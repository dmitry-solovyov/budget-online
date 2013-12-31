using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Helpers
{
    public class CurrencyRateCalculator : ICurrencyRateCalculator
    {
        public ICurrencyRateRepository CurrencyRateRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
        public ICurrentUserProvider CurrentUserProvider { get; set; }

        private IEnumerable<CurrencyRate> _rates;
        private List<Tuple<int, int, decimal, DateTime>> _cachedRates;

        private IEnumerable<Currency> _currencies;
        private IEnumerable<Currency> Currencies
        {
            get { return _currencies ?? (_currencies = Dictionaries.Currencies()); }
        }

        public CurrentCurrencyRate GetRate(int fromCurrencyId, int toCurrencyId)
        {
            var sectionId = CurrentUserProvider.SectionId;

            if (_rates == null)
            {
                _cachedRates = new List<Tuple<int, int, decimal, DateTime>>();

                _rates = CurrencyRateRepository.GetLastRates(sectionId).Where(o => o.IsDisabled == false).ToList();
                _rates = _rates.Concat(
                    _rates.ToList().Where(o => o.Rate != 0)
                        .Select(o => new CurrencyRate
                                         {
                                             BaseCurrencyId = o.TargetCurrencyId,
                                             TargetCurrencyId = o.BaseCurrencyId,
                                             Rate = 1 / o.Rate,
                                             Date = o.Date,
                                             SectionId = o.SectionId,
                                         }));
            }

            Console.WriteLine("Existing rates:");
            foreach (var rate in _rates)
            {
                Console.WriteLine(string.Format("From={0}, To={1} Rate={2}", rate.BaseCurrencyId, rate.TargetCurrencyId, rate.Rate));
            }
            Console.WriteLine("");

            if (fromCurrencyId == toCurrencyId)
            {
                var toCurrency = Currencies.FirstOrDefault(o => o.Id == toCurrencyId);
                if (toCurrency == null)
                    return null;

                return new CurrentCurrencyRate
                           {
                               CurrencyId = toCurrency.Id,
                               CurrencyName = toCurrency.Name,
                               CurrencySymbol = toCurrency.Symbol,
                               Rate = 1m,
                               Date = DateTimeProvider.Now().Date
                           };
            }

            var result = TryToGetDirectRate(_rates.ToList(), fromCurrencyId, toCurrencyId) ??
                            TryToGetTransientRate(_rates.ToList(), fromCurrencyId, toCurrencyId, true);

            return result;
        }

        private Dictionary<int, int> _checkedRates;
        private CurrentCurrencyRate TryToGetTransientRate(IList<CurrencyRate> rates, int fromCurrencyId, int toCurrencyId, bool isInitData = false)
        {
            if (isInitData)
                _checkedRates = new Dictionary<int, int>();

            var ratesFound = rates.Where(o => o.BaseCurrencyId == fromCurrencyId).ToList();
            foreach (var rateFound in ratesFound)
            {
                if (_checkedRates.Any(o => o.Key == rateFound.TargetCurrencyId && o.Value == toCurrencyId))
                    continue;

                _checkedRates.Add(rateFound.TargetCurrencyId, toCurrencyId);

                var baseRate = TryToGetDirectRate(rates, fromCurrencyId, rateFound.TargetCurrencyId);
                if (baseRate == null)
                    continue;

                var rate = TryToGetDirectRate(rates, rateFound.TargetCurrencyId, toCurrencyId) ??
                           TryToGetTransientRate(rates, rateFound.TargetCurrencyId, toCurrencyId);

                if (rate != null)
                {
                    rate.Rate *= baseRate.Rate;

                    Console.WriteLine(string.Format("Found transitive rate. From={0}, To={1}, Rate={2} ", rateFound.TargetCurrencyId, toCurrencyId, rate.Rate));
                    return rate;
                }
            }

            return null;
        }

        private CurrentCurrencyRate TryToGetDirectRate(IEnumerable<CurrencyRate> rates, int fromCurrencyId, int toCurrencyId)
        {
            var currency = Currencies.FirstOrDefault(o => o.Id == toCurrencyId);
            if (currency == null)
                return null;

            var cached = _cachedRates.FirstOrDefault(o => o.Item1 == fromCurrencyId && o.Item2 == toCurrencyId);
            if(cached != null)
            {
                Console.WriteLine(string.Format("Found cached rate. From={0}, To={1}, Rate={2} ", fromCurrencyId, toCurrencyId, cached.Item3));
                return new CurrentCurrencyRate
                            {
                                CurrencyId = currency.Id,
                                CurrencyName = currency.Name,
                                CurrencySymbol = currency.Symbol,
                                Rate = cached.Item3,
                                Date = cached.Item4
                            };
            }


            var rate = rates.FirstOrDefault(o => o.TargetCurrencyId == toCurrencyId && o.BaseCurrencyId == fromCurrencyId);
            if (rate != null)
            {
                _cachedRates.Add(new Tuple<int, int, decimal, DateTime>(fromCurrencyId, toCurrencyId, rate.Rate, rate.Date));

                Console.WriteLine(string.Format("Found direct rate. From={0}, To={1}, Rate={2} ", fromCurrencyId, toCurrencyId, rate.Rate));
                return new CurrentCurrencyRate
                            {
                                CurrencyId = currency.Id,
                                CurrencyName = currency.Name,
                                CurrencySymbol = currency.Symbol,
                                Rate = rate.Rate,
                                Date = rate.Date
                            };
            }

            return null;
        }

        public IEnumerable<Transaction> ConvertCurrency(IEnumerable<Transaction> transactions, int targetCurrencyId)
        {
            var result = new List<Transaction>();
            foreach (var transaction in transactions)
            {
                if (transaction.CurrencyId == targetCurrencyId)
                    result.Add(transaction);
                else
                {
                    var rate = GetRate(transaction.CurrencyId, targetCurrencyId);

                    if (rate != null)
                    {
                        var newTransaction = Fasterflect.CloneExtensions.DeepClone(transaction);

                        newTransaction.CurrencyId = rate.CurrencyId;
                        newTransaction.CurrencyNameSource = rate.CurrencyName;
                        newTransaction.CurrencySymbolSource = rate.CurrencySymbol;
                        newTransaction.Sum = newTransaction.Sum * rate.Rate;

                        result.Add(newTransaction);
                    }
                    else
                        throw new Exception("Rate not found");
                }
            }

            return result
                .GroupBy(o => new
                {
                    o.CurrencyId,
                    o.CurrencyNameSource,
                    o.CurrencySymbolSource,
                    o.CurrencyNameTarget,
                    o.CurrencySymbolTarget,
                    o.Date,
                    o.CategoryId,
                    o.CategoryName,
                    o.AccountId,
                    o.AccountNameSource,
                    o.AccountNameTarget
                })
                .Select(o => new Transaction
                {
                    Amount = o.Sum(x => x.Amount),
                    Sum = o.Sum(x => x.Sum),
                    Date = o.Key.Date,
                    AccountId = o.Key.AccountId,
                    AccountNameSource = o.Key.AccountNameSource,
                    AccountNameTarget = o.Key.AccountNameTarget,
                    CategoryId = o.Key.CategoryId,
                    CategoryName = o.Key.CategoryName,
                    CurrencyId = o.Key.CurrencyId,
                    CurrencyNameSource = o.Key.CurrencyNameSource,
                    CurrencySymbolSource = o.Key.CurrencySymbolSource,
                    CurrencyNameTarget = o.Key.CurrencyNameTarget,
                    CurrencySymbolTarget = o.Key.CurrencySymbolTarget,
                });
        }

        public IEnumerable<TransactionTotal> ConvertCurrency(IEnumerable<TransactionTotal> transactions, int targetCurrencyId)
        {
            var result = new List<TransactionTotal>();
            foreach (var transaction in transactions)
            {
                if (transaction.CurrencyId == targetCurrencyId)
                    result.Add(transaction);
                else
                    if (transaction.CurrencyId.HasValue)
                    {
                        var rate = GetRate(transaction.CurrencyId.Value, targetCurrencyId);

                        if (rate != null)
                        {
                            var newTransaction = Fasterflect.CloneExtensions.DeepClone(transaction);

                            newTransaction.CurrencyId = rate.CurrencyId;
                            newTransaction.CurrencyName = rate.CurrencyName;
                            newTransaction.CurrencySymbol = rate.CurrencySymbol;
                            newTransaction.Sum = newTransaction.Sum * rate.Rate;

                            result.Add(newTransaction);
                        }
                        else
                            throw new Exception(string.Format("Rate is not found (From: {0}, To: {1})", transaction.CurrencyId.Value, targetCurrencyId));
                    }
            }

            return result
                .GroupBy(o => new
                {
                    o.CurrencyId,
                    o.CurrencyName,
                    o.CurrencySymbol,
                    o.Date,
                    o.CategoryId,
                    o.CategoryName,
                    o.AccountId,
                    o.AccountName
                })
                .Select(o => new TransactionTotal
                {
                    Sum = o.Sum(x => x.Sum),
                    Date = o.Key.Date,
                    AccountId = o.Key.AccountId,
                    AccountName = o.Key.AccountName,
                    CategoryId = o.Key.CategoryId,
                    CategoryName = o.Key.CategoryName,
                    CurrencyId = o.Key.CurrencyId,
                    CurrencyName = o.Key.CurrencyName,
                    CurrencySymbol = o.Key.CurrencySymbol,
                });
        }

    }
}
