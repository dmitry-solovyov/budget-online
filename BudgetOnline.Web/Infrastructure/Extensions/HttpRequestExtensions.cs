using System.Web.Mvc;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;

namespace System.Web
{
	public static class HttpRequestExtensions
	{
		public static UserModel CurrentUser(this HttpRequest request)
		{
			var membershipHelper = DependencyResolver.Current.GetService<IMembershipHelper>();
			if (membershipHelper == null)
				return null;

			var currentUser = membershipHelper.GetUser();
			return currentUser;
		}
	}
}