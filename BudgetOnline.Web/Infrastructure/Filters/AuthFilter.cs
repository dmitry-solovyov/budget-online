using System.Web;
using System.Web.Mvc;
using BudgetOnline.Common.Enums;
using BudgetOnline.Contracts;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Security.Api;
using BudgetOnline.Web.Infrastructure.Security;

namespace BudgetOnline.Web.Infrastructure.Filters
{
    public class AuthFilter : AuthorizeAttribute
    {
        private const string ApiTokenKey = "ApiToken";

        public IApiSessionProvider ApiSessionProvider { get; set; }
        public IMembershipHelper MembershipHelper { get; set; }
        public ISessionWrapper SessionWrapper { get; set; }
        public IAuthenticationDataHelper AuthenticationDataHelper { get; set; }

        private string GetTokenFromSession()
        {
            return SessionWrapper.Get<string>(ApiTokenKey);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var token = GetTokenFromSession();
                if (string.IsNullOrWhiteSpace(token))
                {
                    var currentUser = MembershipHelper.CurrentUser;
                    if (currentUser == null)
                        return;

                    var checkResult = AuthenticationDataHelper.CheckAccount(currentUser.Id);
                    if (checkResult.Status != AccountCheckStatus.Ok)
                        return;

                    token = ApiSessionProvider.StartSession(currentUser.Email, checkResult.UserPassword.Password);
                    SessionWrapper.Put(ApiTokenKey, token);
                }
                else
                {
                    var checkResult = AuthenticationDataHelper.UserFromToken(token);
                    if (checkResult.Status != AccountCheckStatus.Ok)
                        return;
                }

                filterContext.HttpContext.Response.Headers["Authorization"] = "Basic " + token;
                if (filterContext.HttpContext.Response.Cookies["Authorization"] == null)
                {
                    filterContext.HttpContext.Response.Cookies.Add(new HttpCookie("Authorization", "Basic " + token));
                }
            }
        }
    }
}