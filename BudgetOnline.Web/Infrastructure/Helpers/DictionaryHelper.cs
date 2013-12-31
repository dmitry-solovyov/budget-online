using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BudgetOnline.UI.Models.SelectItems;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
	public static class DictionaryHelper
	{
		public static SelectItemsModel GetDictionary<T>(
			Func<IEnumerable<T>> itemGetter,
			Func<T, string> idGetter,
			Func<T, string> textGetter,
			Func<T, bool> defaultGetter,
			string defaultId = null
		)
			where T : class
		{
			var items = itemGetter().ToList();

			if (string.IsNullOrWhiteSpace(defaultId) || defaultId.Equals(default(int).ToString(CultureInfo.CurrentUICulture)))
			{
				var defaultRecord = items.FirstOrDefault(defaultGetter);
				if (defaultRecord != null)
					defaultId = idGetter(defaultRecord);
			}

			return new SelectItemsModel(
				items.Select(o => new SelectItemModel { Value = idGetter(o), Text = textGetter(o), Selected = idGetter(o) == defaultId }));
		}

		public static SelectItemsModel GetDictionary<T>(
			Func<IEnumerable<T>> itemGetter,
			Func<T, string> idGetter,
			Func<T, string> textGetter,
			Func<T, string> iconGetter,
			Func<T, bool> defaultGetter,
			string defaultId = null
		)
			where T : class
		{
			var items = itemGetter().ToList();

			if (string.IsNullOrWhiteSpace(defaultId) || defaultId.Equals(default(int).ToString(CultureInfo.CurrentUICulture)))
			{
				var defaultRecord = items.FirstOrDefault(defaultGetter);
				if (defaultRecord != null)
					defaultId = idGetter(defaultRecord);
			}

			return new SelectItemsModel(
				items.Select(o => new SelectItemModel { Value = idGetter(o), Text = textGetter(o), Icon = iconGetter(o), Selected = idGetter(o) == defaultId }));
		}
	}
}