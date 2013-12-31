namespace BudgetOnline.Common
{
	public static class StringExtensions
	{
		public static int TryToInt(this string s, int defaultValue = 0)
		{
			if (!string.IsNullOrWhiteSpace(s))
			{
				int i;
				if (int.TryParse(s, out i))
					return i;
			}

			return defaultValue;
		}
	}
}