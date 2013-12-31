using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class SettingRepository : InternalRepository<Setting, Types.Simple.Setting>, ISettingRepository
	{
		public override Table<Setting> Source
		{
			get { return DatabaseContext.Get().Settings; }
		}

		public Types.Simple.Setting FindByName(int? sectionId, int? userId, string name)
		{
			var localItems = GetListInternal().Where(o => o.Name == name && o.IsDisabled == false);

			if (userId.HasValue)
				localItems = localItems.Where(o => (o.UserId == null || o.UserId.Equals(userId.Value)));
			if (sectionId.HasValue)
				localItems = localItems.Where(o => (o.SectionId == null || o.SectionId.Equals(sectionId.Value)));

			var items = GetMappedItems(localItems).ToList();

			var result = items.FirstOrDefault(o => o.SectionId == sectionId && o.UserId == userId && o.Name.Equals(name));
			if (result != null)
				return result;

			result = items.FirstOrDefault(o => o.SectionId == sectionId && o.UserId == null && o.Name.Equals(name));
			if (result != null)
				return result;

			result = items.FirstOrDefault(o => o.SectionId == null && o.UserId == null && o.Name.Equals(name));
			if (result != null)
				return result;

			return null;
		}

		public IEnumerable<Types.Simple.Setting> GetSettings(int sectionId, int userId)
		{
			return GetList(o => o.SectionId == null || (o.SectionId == sectionId && (o.UserId == userId || o.UserId == null)) && o.IsDisabled == false);
		}

		public void Set<T>(int sectionId, int? userId, string name, T value)
		{
			var setting = GetSingle(o => o.SectionId == sectionId && o.UserId == userId && o.Name == name && o.IsDisabled == false);
			if (setting == null)
			{
				Insert(new Types.Simple.Setting
						{
							Name = name,
							SectionId = sectionId,
							UserId = userId,
							Value = value.ToString(),
							IsDisabled = false,
							CreatedWhen = DateTime.UtcNow,
							CreatedBy = userId
						});
			}
			else
			{
				UpdateInternal(
					o => o.SectionId == sectionId && o.UserId == userId && o.Name == name && o.IsDisabled == false,
					settingInternal =>
					{
						settingInternal.Value = value.ToString();
					});
			}
		}

		public void Reset(int sectionId, int userId, string name)
		{
			UpdateInternal(
					o => o.SectionId == sectionId && o.UserId == userId && o.Name == name && o.IsDisabled == false,
					settingInternal =>
					{
						settingInternal.IsDisabled = true;
					});
		}
	}
}
