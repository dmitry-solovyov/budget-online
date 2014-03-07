using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public class ApiSessionProvider : IApiSessionProvider
    {
        public IUserConnectRepository UserConnectRepository { get; set; }
        public IAuthTokenHelper AuthTokenHelper { get; set; }
        public IAuthenticationDataHelper AuthenticationDataHelper { get; set; }

        public User CurrentUser
        {
            get
            {
                var token = AuthTokenHelper.GetToken();

                var userInfo = AuthenticationDataHelper.UserFromToken(token);
                if (userInfo != null && userInfo.Status == AccountCheckStatus.Ok)
                    return userInfo.User;

                return null;
            }
        }

        public string StartSession(string userName, string password)
        {
            var checkResult = AuthenticationDataHelper.ValidateLogin(userName, password);
            if (checkResult.Status == AccountCheckStatus.Ok)
            {
                var token = AuthTokenHelper.GenerateToken(userName, password);

                AuthenticationDataHelper.TrackUsersToken(userName, token, checkResult.UserPassword.Id);

                return token;
            }

            return null;
        }

        public void UpdateTokenUsage()
        {
            var token = AuthTokenHelper.GetToken();

            var userInfo = AuthenticationDataHelper.UserFromToken(token);
            if (userInfo != null && userInfo.Status == AccountCheckStatus.Ok)
            {
                UserConnectRepository.UpdateTokenUsage(userInfo.UserConnect.Id);
            }
        }
    }
}