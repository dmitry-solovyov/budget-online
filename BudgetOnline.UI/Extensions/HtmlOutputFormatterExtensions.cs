using System;
using System.Globalization;

namespace BudgetOnline.UI.Extensions
{
	public static class HtmlOutputFormatterExtensions
	{
		public enum OutputLengthType
		{
			Short,
			Normal,
			Long
		}

		public static string ToHtmlString(this DateTime value, OutputLengthType lengthType)
		{
			switch (lengthType)
			{
				case OutputLengthType.Short:
					return value.ToLocalTime().ToShortDateString() + (value.Hour > 0 || value.Minute > 0 ? " " + value.ToLocalTime().ToShortTimeString() : string.Empty);
				case OutputLengthType.Long:
					return value.ToLocalTime().ToLongDateString() + (value.Hour > 0 || value.Minute > 0 ? " " + value.ToLocalTime().ToLongTimeString() : string.Empty);
				default:
					return value.ToLocalTime().ToString(CultureInfo.CurrentUICulture);
			}
		}

		public static string ToHtmlString(this decimal value)
		{
			return ToHtmlString(value, OutputLengthType.Normal);
		}

		public static string ToHtmlString(this decimal value, OutputLengthType lengthType)
		{
			switch (lengthType)
			{
                //case OutputLengthType.Long:
                //    return Math.Round(value, 2).ToString("### ### ### ##0.00", CultureInfo.CurrentUICulture);
				default:
                    return Math.Round(value, 2).ToString("### ### ### ##0.00", CultureInfo.CurrentUICulture);
			}
		}
	}
}