using BudgetOnline.Api.Infrastructure.Security;

namespace BudgetOnline.Api.Controllers
{
    //authorization filter integrated with Autofac
    public class BaseApiAuthController : BaseApiController
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }
        protected Data.Manage.Types.Simple.User CurrentUser
        {
            get
            {
                return CurrentApiUserProvider.CurrentUser;
            }
        }
    }
}
