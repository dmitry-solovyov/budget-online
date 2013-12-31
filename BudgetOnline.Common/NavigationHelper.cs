using System;
using System.Web;

namespace BudgetOnline.Common
{
	public static class NavigationHelper
	{
		public enum ShowActiveLevels
		{
			ControllerLevel,
			ControllerAndActionLevel,
			ActionLevel,
			AreaLevel,
		}

		public static bool IsPathActive(ShowActiveLevels level, string controller, string action, string area = null)
		{
			var routeData = HttpContext.Current.Request.RequestContext.RouteData;

			var currentAction = (string)(routeData.DataTokens["controller"] ?? routeData.GetRequiredString("action"));
			var currentController = (string)(routeData.DataTokens["controller"] ?? routeData.GetRequiredString("controller"));
			var currentArea = routeData.DataTokens["area"] as string;

			switch (level)
			{
				case ShowActiveLevels.ControllerLevel:
					if (string.Equals(currentController, controller, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				case ShowActiveLevels.ControllerAndActionLevel:
					if (string.Equals(currentAction, action, StringComparison.InvariantCultureIgnoreCase) &&
						string.Equals(currentController, controller, StringComparison.InvariantCultureIgnoreCase) &&
						string.Equals(currentArea, area, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				case ShowActiveLevels.ActionLevel:
					if (string.Equals(currentAction, action, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				case ShowActiveLevels.AreaLevel:
					if (!string.IsNullOrWhiteSpace(area) && string.Equals(currentArea, area, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				default:
					return false;
			}


			return false;
		}
	}
}
