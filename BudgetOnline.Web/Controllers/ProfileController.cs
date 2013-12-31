using System.Web.Mvc;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Controllers
{
	public class ProfileController : SecuredController
	{
		public ActionResult Index()
		{
			var model = PopulateViewModel();

			return View("~/Views/Profile/Index.cshtml", model);
		}

		private ProfileViewModel PopulateViewModel()
		{
			var result = new ProfileViewModel();
			
			var user = MembershipHelper.GetUser();
			result.Email = user.Email;
			result.Id = user.Id;
			result.AllowChangePassword = false;
			result.IsDisabled = user.IsDisabled;

			return result;
		}
	}
}