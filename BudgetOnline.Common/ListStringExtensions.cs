using System.Collections.Generic;

namespace BudgetOnline.Common
{
	public static class ListStringExtensions
	{
		public static string JoinedString(this List<string> list)
		{
			return JoinedString(list, string.Empty);
		}

		public static string JoinedString(this List<string> list, string separator)
		{
			return string.Join(separator, list);
		}
	}
}