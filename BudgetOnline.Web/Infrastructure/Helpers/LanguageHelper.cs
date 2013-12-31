using System.Collections.Generic;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
	public class LanguageHelper
	{
		public static IEnumerable<string> GetLanguageNames()
		{
			yield return "RU";
			yield return "EN";
		}
	}
}