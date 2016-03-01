using BudgetOnline.Api.Common.Controllers;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Common
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
