using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.Models;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Controllers
{
    public class SessionController : BaseApiController
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginPost()
        {
            var request = GetPostData<SessionLoginRequest>();
            var token = CurrentApiUserProvider.StartSession(request.UserName, request.Password);

            if (string.IsNullOrWhiteSpace(token))
                return PrepareResponse(HttpStatusCode.NotFound);

            var response = new SessionLoginResponse
                               {
                                   Success = true,
                                   Token = token
                               };

            return PrepareResponse(response);
        }

        [HttpGet]
        [ActionName("validate")]
        public HttpResponseMessage ValidateGet()
        {
            if (CurrentApiUserProvider.CurrentUser == null)
                return PrepareResponse(HttpStatusCode.Forbidden);

            var response = new SessionValidationResponse
            {
                Valid = true
            };

            return PrepareResponse(response);
        }

        [HttpGet]
        [ActionName("me")]
        public HttpResponseMessage MeGet()
        {
            if(CurrentApiUserProvider.CurrentUser == null)
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            var user = new User { Name = CurrentApiUserProvider.CurrentUser.Email };

            return PrepareResponse(user);
        }
    }
}
