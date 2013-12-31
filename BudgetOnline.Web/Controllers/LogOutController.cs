using System.Web.Mvc;
using BudgetOnline.Web.Infrastructure.Navigation;

namespace BudgetOnline.Web.Controllers
{
    public class LogOutController : BaseController
    {
        public ActionResult Index()
        {
			AuthProvier.LogoutCurrentUser();

			return NavigationHelper.GetSiteDefaultPath();
        }

    }
}
