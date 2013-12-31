using System.Configuration;

namespace BudgetOnline.Web.Infrastructure
{
	public static class AppSettings
	{
		private const string formatNumber = "### ### ### ### ##0.00";
		public static string FormatNumber
		{
			get { return formatNumber; }
		}
		public static string FormatDate
		{
			get { return "dd.MM.yyyy"; }
		}
		public static string FormatDateShort
		{
			get { return "dd.MM.yy"; }
		}
		 

		public static bool TurnOffCacheForDictionaries
		{
			get {
				var configValue = ConfigurationManager.AppSettings["TurnOffCacheForDictionaries"];
				bool result;
				if (bool.TryParse(configValue, out result))
					return result;

				return false;
			}
		}
	}
}