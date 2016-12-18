using System.Web.Mvc;

namespace BudgetOnline.Web.Controllers
{
	public class WelcomeController : SecuredController
	{
		public ActionResult Index()
		{
			return View("~/Views/Welcome/Index.cshtml");
		}

        public ActionResult React()
		{
            return View("~/Views/Welcome/React.cshtml");
		}

		public ActionResult TestCallback()
		{
			return Content("AAA");
		}
	}
}
