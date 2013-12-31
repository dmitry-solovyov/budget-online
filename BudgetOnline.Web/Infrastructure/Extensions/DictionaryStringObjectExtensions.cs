using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetOnline.Web.Infrastructure.Extensions
{
	public static class DictionaryStringObjectExtensions
	{
		public static string GetEntry(this  Dictionary<string, object> dictionary, string key)
		{
			if (dictionary.ContainsKey(key))
				return dictionary[key] as string;

			return string.Empty;
		}
	}
}