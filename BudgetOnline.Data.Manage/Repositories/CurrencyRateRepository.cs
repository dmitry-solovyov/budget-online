using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using CurrencyRate = BudgetOnline.Data.MSSQL.CurrencyRate;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class CurrencyRateRepository : InternalRepository<CurrencyRate, Types.Simple.CurrencyRate>, ICurrencyRateRepository
	{
		#region Overrides of InternalRepository<CurrencyRate,CurrencyRate>

		public override Table<CurrencyRate> Source
		{
			get { return DatabaseContext.Get().CurrencyRates; }
		}

		#endregion

		#region Implementation of ICurrencyRateRepository

		public IEnumerable<Types.Simple.CurrencyRate> GetList(int sectionId)
		{
			var db = DatabaseContext.Get();

			var items = from rate in db.CurrencyRates

						join tTrg in db.Currencies on rate.TargetCurrencyId equals tTrg.Id into targetCurrency
						from tTarget in targetCurrency.DefaultIfEmpty()

						join tBs in db.Currencies on rate.BaseCurrencyId equals tBs.Id into baseCurrency
						from tBase in baseCurrency.DefaultIfEmpty()

						where rate.SectionId == sectionId && rate.IsDisabled == false
						orderby rate.Date descending
						select new Types.Simple.CurrencyRate
						{
							Id = rate.Id,
							BaseCurrencyId = rate.BaseCurrencyId,
							TargetCurrencyId = rate.TargetCurrencyId,

							SectionId = rate.SectionId,
							Rate = rate.Rate,
							IsDisabled = rate.IsDisabled,
							Date = rate.Date,

							TargetCurrencyName = tTarget.Name,
							TargetCurrencySymbol = tTarget.Symbol,

							BaseCurrencyName = tBase.Name,
							BaseCurrencySymbol = tBase.Symbol,
						};

			return items;
		}

		public IEnumerable<Types.Simple.CurrencyRate> GetLastRates(int sectionId)
		{
			var allRates = GetList(sectionId).ToList();

			var rates = allRates
				.GroupBy(o => new { o.TargetCurrencyId, o.TargetCurrencyName, o.TargetCurrencySymbol, o.BaseCurrencyId, o.BaseCurrencyName, o.BaseCurrencySymbol })
				.Select(o => new Types.Simple.CurrencyRate
								 {
									 BaseCurrencyId = o.Key.BaseCurrencyId,
									 BaseCurrencyName = o.Key.BaseCurrencyName,
									 BaseCurrencySymbol = o.Key.BaseCurrencySymbol,
									 TargetCurrencyId = o.Key.TargetCurrencyId,
									 TargetCurrencyName = o.Key.TargetCurrencyName,
									 TargetCurrencySymbol = o.Key.TargetCurrencySymbol,
									 Date = o.Max(r => r.Date),
								 }).ToList();

			rates.AsParallel().ForAll(item =>
										  {
											  item.Rate = allRates
													.Where(o => o.Date == item.Date && o.TargetCurrencyId == item.TargetCurrencyId &&
																o.BaseCurrencyId == item.BaseCurrencyId)
													.Select(o => o.Rate)
													.FirstOrDefault();
										  });

			return rates;
		}

		public void Update(Types.Simple.CurrencyRate row)
		{
			UpdateInternal(
				o => o.Id == row.Id,
				record =>
				{
					record.IsDisabled = row.IsDisabled;
					record.UpdatedWhen = DateTime.UtcNow;
					record.UpdatedBy = row.UpdatedBy;
					record.Rate = row.Rate;
					record.TargetCurrencyId = row.TargetCurrencyId;
					record.BaseCurrencyId = row.BaseCurrencyId;
				});
		}

		public Types.Simple.CurrencyRate Get(int id)
		{
			return base.GetSingle(o => o.Id == id);
		}

		#endregion
	}
}
