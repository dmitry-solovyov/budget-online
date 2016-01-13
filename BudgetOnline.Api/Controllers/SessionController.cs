using System;
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
        [Route("session/login")]
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
        [Route("session/validate")]
        public HttpResponseMessage ValidateGet()
        {
            if (CurrentApiUserProvider.CurrentSession == null)
            {
                return PrepareResponse(HttpStatusCode.Forbidden);
            }

            var currentSession = CurrentApiUserProvider.CurrentSession;

            var response = new SessionValidationResponse
            {
                Valid = currentSession != null,
                ExpiredAfter = currentSession != null ? currentSession.ExpiresWhen : new DateTime()
            };

            return PrepareResponse(response);
        }

        [HttpGet]
        [Route("session/me")]
        public HttpResponseMessage MeGet()
        {
            if (CurrentApiUserProvider.CurrentSession == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            var user = new User { Name = CurrentApiUserProvider.CurrentSession.User.Email };

            return PrepareResponse(user);
        }
    }
}
