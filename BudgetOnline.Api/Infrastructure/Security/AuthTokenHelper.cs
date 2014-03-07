using System;
using System.Text;
using System.Web;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public class AuthTokenHelper : IAuthTokenHelper
    {
        public string GenerateToken(string userName, string password)
        {
            return EncodeToken(Guid.NewGuid().ToString());
        }

        public string GetAuthorizationHeader()
        {
            return HttpContext.Current.Request.Headers["Authorization"];
        }

        public string GetToken()
        {
            var authHeader = GetAuthorizationHeader();

            if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("basic ", StringComparison.InvariantCultureIgnoreCase))
            {
                var splittedToken = authHeader.Split(' ');
                if (splittedToken.Length == 2)
                {
                    var token = splittedToken[1];
                    return token;
                }
            }

            return null;
        }

        private string EncodeToken(string text)
        {
            //var encoding = Encoding.GetEncoding("iso-8859-1");
            var encoding = Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(text));
        }

        private string DecodeToken(string text)
        {
            var encoding = Encoding.UTF8;
            return encoding.GetString(Convert.FromBase64String(text));
        }
    }
}