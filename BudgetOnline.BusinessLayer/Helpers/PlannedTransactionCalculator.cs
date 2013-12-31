using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.BusinessLayer.Helpers
{
	public class PlannedTransactionCalculator : IPlannedTransactionCalculator
	{
		public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
		public IPlannedTransactionRepository PlannedTransactionRepository { get; set; }
		public IPeriodTypeRepository PeriodTypeRepository { get; set; }
		public IDateTimeProvider DateTimeProvider { get; set; }
		public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }

		private IEnumerable<PeriodType> _periodTypes;
		private IEnumerable<PeriodType> GetPeriodTypes(int sectionId)
		{
			return _periodTypes ?? (_periodTypes = PeriodTypeRepository.GetList(sectionId).ToList());
		}

		private IEnumerable<TransactionTotal> GetActualTotals(int sectionId, DateTime onDate)
		{
			// calculate totals by fact
			var transactionOptions = new TransactionStatisticsSearchOptions { Date2 = onDate };
			return TransactionStatisticsRepository.GetTotalsByCurrencies(sectionId, transactionOptions);
		}

		public IEnumerable<TransactionTotal> GenerateActual(int sectionId, PlannedTransactionSearchOptions options, int? targetCurrencyId, bool useActualTotals)
		{
			// get planned transactions
			var plannedTransactions = PlannedTransactionRepository.GetList(sectionId, options);
			var totals = GenerateActualTransacions(sectionId, options.Date1.Value, options.Date2.Value, plannedTransactions);

			if (useActualTotals)
			{
				// calculate totals by fact
				var getActual = GetActualTotals(sectionId, options.Date2.Value);

				// merge fact with calculated plan
				totals = getActual.Concat(totals).ToList();
			}

			if (targetCurrencyId.HasValue)
				totals = CurrencyRateCalculator.ConvertCurrency(totals, targetCurrencyId.Value);

			return GroupBy(totals, TransactionTotalGroupTypes.MonthCurrencySign);
		}

		public IEnumerable<TransactionTotal> GenerateActualByPlannedTransaction(int sectionId, DateTime fromDate, DateTime toDate, int transactionId)
		{
			var plannedTransaction = PlannedTransactionRepository.GetById(transactionId);
			if (plannedTransaction == null)
				return Enumerable.Empty<TransactionTotal>();

			var generatedTotals = GenerateActualTransacions(sectionId, fromDate, toDate, new[] { plannedTransaction });

			return GroupBy(generatedTotals, TransactionTotalGroupTypes.MonthCurrencySign);
		}

		public IEnumerable<PlannedTransaction> FindPlansRelatedToDate(int sectionId, DateTime fromDate, DateTime toDate)
		{
			var planningOptions = new PlannedTransactionSearchOptions { Date1 = DateTimeProvider.Now() };

			var plannedTransactions = PlannedTransactionRepository.GetList(sectionId, planningOptions).ToList();

			var actualTotals = GenerateActualTransacions(sectionId, fromDate, toDate, plannedTransactions);
			var actualTotalsRelatedIds = actualTotals.SelectMany(o => o.RelatedTransactionIds);

			var result = plannedTransactions.Where(o => actualTotalsRelatedIds.Contains(o.Id));
			if (fromDate.Year == DateTimeProvider.Now().Year && fromDate.Month == DateTimeProvider.Now().Month)
			{
                var actual = GetActualTotals(sectionId, DateTimeProvider.Now(DateTimeKind.Utc).Date)
					.Select(o => new PlannedTransaction { FromDate = o.Date.Value, CurrencyId = o.CurrencyId.Value, CurrencyName = o.CurrencyName, CurrencySymbol = o.CurrencySymbol, Sum = o.Sum, Description = "Текущий баланс" });

				result = result.Concat(actual);
			}

			return result;
		}

		public IEnumerable<TransactionTotal> GroupBy(IEnumerable<TransactionTotal> transactions, TransactionTotalGroupTypes groupType)
		{
			if (transactions == null)
				throw new ArgumentNullException("transactions");

			switch (groupType)
			{
				case TransactionTotalGroupTypes.MonthSign:
					return transactions
						.Where(o => o.Date.HasValue)
						.GroupBy(o => new { Date = new DateTime(o.Date.Value.Year, o.Date.Value.Month, 1), o.SumNegative })
						.Select(o => new TransactionTotal
						{
							Date = o.Key.Date,
							Sum = o.Sum(r => r.Sum)
						})
						.OrderBy(o => o.Date)
						.ToList();

				case TransactionTotalGroupTypes.MonthCurrencySign:
					return transactions
						.Where(o => o.Date.HasValue)
						.GroupBy(o => new { Date = new DateTime(o.Date.Value.Year, o.Date.Value.Month, 1), o.CurrencyId, o.CurrencyName, o.CurrencySymbol, o.SumNegative })
						.Select(o => new TransactionTotal
						{
							CurrencyId = o.Key.CurrencyId,
							CurrencyName = o.Key.CurrencyName,
							CurrencySymbol = o.Key.CurrencySymbol,
							Date = o.Key.Date,
							Sum = o.Sum(r => r.Sum)
						})
						.OrderBy(o => o.Date)
						.ThenBy(o => o.CurrencyName)
						.ToList();

				default:
					throw new NotImplementedException();
			}
		}

		private IEnumerable<TransactionTotal> GenerateActualTransacions(int sectionId
			, DateTime fromDate
			, DateTime toDate
			, IEnumerable<PlannedTransaction> items)
		{
			return items.SelectMany(item => ProcessPlannedTransaction(sectionId, fromDate, toDate, item));
		}

		private IEnumerable<TransactionTotal> ProcessPlannedTransaction(int sectionId, DateTime fromDate, DateTime toDate, PlannedTransaction item)
		{
			if (!item.IsDisabled)
			{
				var periodType = GetPeriodTypes(sectionId).FirstOrDefault(o => o.Id == item.PeriodTypeId);
				if (periodType == null)
					throw new NullReferenceException("Period Type is not found");

				var counter = 0;
				DateTime currentDate = item.FromDate;

				while (currentDate <= toDate && counter <= 1000)
				{
					if (currentDate >= fromDate && currentDate <= (item.ToDate ?? DateTime.MaxValue))
						yield return GenerateTransactionRecord(item, currentDate);

					currentDate = CalculateNextDate(periodType, currentDate);
					counter++;
				}
			}
		}

		private DateTime CalculateNextDate(PeriodType periodType, DateTime currentDate)
		{
			var dt = currentDate;

			if (periodType.Hours.HasValue)
				dt = dt.AddHours(periodType.Hours.Value);
			if (periodType.Days.HasValue)
				dt = dt.AddDays(periodType.Days.Value);
			if (periodType.WorkingDays.HasValue)
				dt = dt.AddDays(periodType.WorkingDays.Value); //TODO: need to fix
			if (periodType.Weeks.HasValue)
				dt = dt.AddDays(7 * periodType.Weeks.Value);
			if (periodType.Monthes.HasValue)
				dt = dt.AddMonths(periodType.Monthes.Value);
			if (periodType.Quarters.HasValue)
				dt = dt.AddMonths(3 * periodType.Quarters.Value);
			if (periodType.Years.HasValue)
				dt = dt.AddYears(periodType.Years.Value);

			if (dt == currentDate)
				return DateTime.MaxValue;

			return dt;
		}

		private TransactionTotal GenerateTransactionRecord(PlannedTransaction plannedTransaction, DateTime currentDate)
		{
			return new TransactionTotal
			{
				AccountId = plannedTransaction.AccountId,
				AccountName = plannedTransaction.AccountName,
				CategoryId = plannedTransaction.CategoryId,
				CategoryName = plannedTransaction.CategoryName,
				CurrencyId = plannedTransaction.CurrencyId,
				CurrencyName = plannedTransaction.CurrencyName,
				CurrencySymbol = plannedTransaction.CurrencySymbol,
				Date = currentDate,
				SectionId = plannedTransaction.SectionId,
				Sum = plannedTransaction.Sum,
				RelatedTransactionIds = new[] { plannedTransaction.Id }
			};
		}
	}
}
