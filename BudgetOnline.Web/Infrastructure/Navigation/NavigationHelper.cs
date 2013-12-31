using System.Web.Mvc;
using System.Web.Routing;

namespace BudgetOnline.Web.Infrastructure.Navigation
{
	public class NavigationHelper
	{
		public static RedirectToRouteResult GetSiteDefaultPath()
		{
			var dict = new RouteValueDictionary {{"Controller", ""}, {"Action", ""}};
			return new RedirectToRouteResult(dict);
		}
	}
}