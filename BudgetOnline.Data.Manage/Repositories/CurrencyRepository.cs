using System;
using System.Collections.Generic;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using Currency = BudgetOnline.Data.MSSQL.Currency;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class CurrencyRepository : InternalRepository<Currency, Types.Simple.Currency>, ICurrencyRepository
	{
		#region Overrides of InternalRepository<Currency,Currency>

		public override Table<Currency> Source
		{
			get { return DatabaseContext.Get().Currencies; }
		}

		#endregion

		#region Implementation of ICurrencyRepository

		public IEnumerable<Types.Simple.Currency> GetList(int sectionId)
		{
			return GetList(o => o.SectionId == sectionId);
		}

		public Types.Simple.Currency GetDefault(int sectionId)
		{
			return base.GetSingle(o => o.IsDefault && o.SectionId == sectionId);
		}

		public void Update(Types.Simple.Currency row)
		{
			UpdateInternal(
				o => o.Id == row.Id,
				record =>
				{
					record.IsDisabled = row.IsDisabled;
					record.IsDefault = row.IsDefault;
					record.UpdatedWhen = DateTime.UtcNow;
					record.UpdatedBy = row.UpdatedBy;
					record.Name = row.Name;
					record.Symbol = row.Symbol;
					record.Description = row.Description;
				});
		}

		public Types.Simple.Currency Get(int currencyId)
		{
			return base.GetSingle(o => o.Id == currencyId);
		}

		#endregion
	}
}
