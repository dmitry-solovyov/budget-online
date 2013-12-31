using System.Globalization;

namespace System
{
	public static class DateTimeExtensions
	{
		public static string ToLocalString(this DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
				return string.Empty;

			return dateTime.ToLocalTime().ToString(CultureInfo.CurrentUICulture);
		}

		public static string ToLocalString(this DateTime? dateTime)
		{
			if (!dateTime.HasValue)
				return string.Empty;

			return ToLocalString(dateTime.Value); 
		}
	}
}