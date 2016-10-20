using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.Common.Controllers;
using BudgetOnline.Api.Common.Models;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Logger;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Controllers
{
    public class SessionController : BaseApiController
    {
        public IApiSessionProvider CurrentApiUserProvider { get; set; }
        public ILogWriter LogWriter { get; set; }

        [HttpPost]
        [Route("session/login")]
        public HttpResponseMessage LoginPost()
        {
            var request = GetPostData<SessionLoginRequest>();

            var token = CurrentApiUserProvider.StartSession(request.UserName, request.Password);

            if (string.IsNullOrWhiteSpace(token))
            {
                LogWriter.Debug(string.Format("Login {0} didn't pass validation!", request.UserName));

                return PrepareResponse(HttpStatusCode.NotFound);
            }

            LogWriter.Debug(string.Format("Login {0} passed validation!", request.UserName));

            var response =
                new SessionLoginResponse
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
