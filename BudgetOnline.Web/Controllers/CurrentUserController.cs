using System.Web.Mvc;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Controllers
{
	public class CurrentUserController : BaseController
	{
		[OutputCache(Duration = 300, VaryByCustom = "browser")]
		public ActionResult LoggedOnName()
		{
			var model = new CurrentUserViewModel();

			var membershipUser = MembershipHelper.GetUser();
			if (membershipUser != null)
				if (!string.IsNullOrWhiteSpace(membershipUser.Email))
				{
					model.Email = membershipUser.Email;
					model.IsLoggedOn = true;
				}

			return View(model);
		}
	}
}
