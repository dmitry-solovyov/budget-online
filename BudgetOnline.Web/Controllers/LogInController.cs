using System.Web.Mvc;
using BudgetOnline.Web.Infrastructure.Navigation;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.ViewModels.Security;

namespace BudgetOnline.Web.Controllers
{
	public class LogInController : Controller
	{
        public IAuthProvider AuthProvier { get; set; }

		public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(LogInViewModel model)
        {
			if (AuthProvier.Authenticate(model.UserName, model.Password))
				return NavigationHelper.GetSiteDefaultPath();

			ModelState.AddModelError(
				"login_pass_incorrect", 
				string.Format("User/password incorrect ({0}/{1})", model.UserName, model.Password));

			return View(model);
        }
	}
}
