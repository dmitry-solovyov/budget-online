using System.Web.Mvc;
using BudgetOnline.Web.ViewModels;

namespace BudgetOnline.Web.Controllers
{
    public class DashboardController : SecuredController
    {
        public ActionResult Index()
        {
			var model = new DashboardViewModel();

			return View("~/Views/Dashboard/Index.cshtml", model);
        }

    }
}
