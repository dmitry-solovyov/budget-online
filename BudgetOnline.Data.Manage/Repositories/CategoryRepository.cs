using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using Category = BudgetOnline.Data.MSSQL.Category;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class CategoryRepository : InternalRepository<Category, Types.Simple.Category>, ICategoryRepository
	{
		#region Overrides of InternalRepository<Category,Category>

		public override Table<Category> Source
		{
			get { return DatabaseContext.Get().Categories; }
		}

		#endregion

		#region Implementation of ICategoryRepository

		public IEnumerable<Types.Simple.Category> GetList(int sectionId)
		{
			return GetMappedItems(
				GetListInternal().Where(o => o.SectionId == sectionId).OrderBy(o => o.Name));
		}

		public Types.Simple.Category GetDefault(int sectionId)
		{
			return base.GetSingle(o => o.IsDefault && o.SectionId == sectionId);
		}

		public void Update(Types.Simple.Category row)
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
					record.Description = row.Description;
				});
		}

		public Types.Simple.Category Get(int categoryId)
		{
			return base.GetSingle(o => o.Id == categoryId);
		}

		#endregion
	}
}
