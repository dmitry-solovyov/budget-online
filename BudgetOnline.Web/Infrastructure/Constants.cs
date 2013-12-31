using System;

namespace BudgetOnline.Web.Infrastructure
{
	public static class Constants
	{
		public static bool TraceRequests { get { return true; } }

		public const string UserSessionKey = "CurrentUserInSession";
		public const string UserSecurityInfoSessionKey = "CurrentUserSecuruityInfoInSession";
		public static readonly TimeSpan GetDefaultSearchCacheTimeout = new TimeSpan(0, 5, 0);
	}
}