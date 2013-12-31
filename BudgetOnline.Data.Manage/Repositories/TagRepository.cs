using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using Tag = BudgetOnline.Data.MSSQL.Tag;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class TagRepository : InternalRepository<Tag, Types.Simple.Tag>, ITagRepository
	{
		#region Overrides of InternalRepository<Tag,Tag>

		public override Table<Tag> Source
		{
			get { return DatabaseContext.Get().Tags; }
		}

		#endregion

		#region Implementation of ITagRepository

		public IEnumerable<Types.Simple.Tag> GetList(int sectionId)
		{
			return GetMappedItems(
				GetListInternal().Where(o => o.SectionId == sectionId).OrderBy(o => o.Name));
		}

		public void Update(Types.Simple.Tag row)
		{
			UpdateInternal(
				o => o.Id == row.Id,
				record =>
				{
					record.IsDisabled = row.IsDisabled;
					record.UpdatedWhen = DateTime.UtcNow;
					record.UpdatedBy = row.UpdatedBy;
					record.Name = row.Name;
					record.Hits = row.Hits;
				});
		}

		public Types.Simple.Tag Get(int tagId)
		{
			return base.GetSingle(o => o.Id == tagId);
		}

		#endregion
	}
}
