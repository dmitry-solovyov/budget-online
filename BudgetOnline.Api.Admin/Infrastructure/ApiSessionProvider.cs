using System;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Admin.Infrastructure
{
    public class ApiSessionProvider : IApiSessionProvider
    {
        public IUserConnectRepository UserConnectRepository { get; set; }
        public IAuthTokenHelper AuthTokenHelper { get; set; }
        public IAuthenticationDataHelper AuthenticationDataHelper { get; set; }

        public SessionInfo CurrentSession
        {
            get
            {
                var token = AuthTokenHelper.GetToken();

                var userInfo = AuthenticationDataHelper.UserFromToken(token);
                if (userInfo != null && userInfo.Status == AccountCheckStatuses.Ok)
                {
                    var connectInfo = AuthenticationDataHelper.GetUserConnect(token);
                    if (connectInfo != null)
                    {
                        AuthenticationDataHelper.UpdateConnectUsage(connectInfo);

                        return new SessionInfo
                        {
                            Id = connectInfo.Id,
                            ExpiresWhen = connectInfo.ExpiresWhen ?? DateTime.MaxValue,
                            User = userInfo.User
                        };
                    }
                }

                return null;
            }
        }

        public string StartSession(string userName, string password)
        {
            var checkResult = AuthenticationDataHelper.ValidateLogin(userName, password);
            if (checkResult.Status == AccountCheckStatuses.Ok)
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
            
            if (userInfo != null && userInfo.Status == AccountCheckStatuses.Ok)
            {
                var dt = userInfo.UserConnect.ExpiresWhen;
                if (dt.HasValue)
                {
                    dt = dt.Value.Add(AuthenticationDataHelper.GetTokenValidityPeriod(userInfo.User.SectionId));
                }

                UserConnectRepository.UpdateTokenUsage(userInfo.UserConnect.Id, dt ?? DateTime.Now);
            }
        }
    }
}