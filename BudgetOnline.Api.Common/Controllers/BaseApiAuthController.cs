using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Common.Controllers
{
    //authorization filter integrated with Autofac
    public class BaseApiAuthController : BaseApiController
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }
        protected SessionInfo CurrentSession
        {
            get
            {
                return CurrentApiUserProvider.CurrentSession;
            }
        }
    }
}
