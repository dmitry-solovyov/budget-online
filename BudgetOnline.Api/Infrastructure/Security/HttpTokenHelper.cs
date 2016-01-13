using System;
using System.Text;
using System.Web;
using BudgetOnline.Api.Models;

namespace BudgetOnline.Api.Infrastructure.Security
{
    public static class HttpTokenHelper
    {
        public static string GenerateToken(string userName, Guid sessionId)
        {
            string tokenContent = string.Concat(userName, Consts.AuthorizationHeaderPartsSeparator, sessionId.ToString("N"));

            return EncodeToken(tokenContent);
        }

        public static TokenParts GetTokenParts(HttpRequest request)
        {
            string token = request.Headers.Get(Consts.AuthorizationHeaderKey);

            if (string.IsNullOrWhiteSpace(token))
                return null;

            return GetTokenParts(token);
        }

        public static TokenParts GetTokenParts(string token)
        {
            string[] parts = ParseToken(token);

            if (parts != null && parts.Length == 2)
                return new TokenParts
                {
                    UserName = parts[0],
                    SessionId = Guid.Parse(parts[1])
                };

            return null;
        }

        private static string[] ParseToken(string token)
        {
            if (token.StartsWith(Consts.HeaderSchema + " ", StringComparison.InvariantCultureIgnoreCase))
                // remove schema type from header's value
                token = token.Substring((Consts.HeaderSchema + " ").Length);

            string encoded = DecodeToken(token);
            if (!string.IsNullOrWhiteSpace(encoded))
            {
                if (encoded.Contains(Consts.AuthorizationHeaderPartsSeparator))
                    return encoded.Split(new[] { Consts.AuthorizationHeaderPartsSeparator }, StringSplitOptions.RemoveEmptyEntries);

                return new[] { encoded };
            }


            return new string[0];
        }

        private static string DecodeToken(string text)
        {
            try
            {
                var encoding = Encoding.UTF8;
                return encoding.GetString(Convert.FromBase64String(text));
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string EncodeToken(string text)
        {
            try
            {
                var encoding = Encoding.UTF8;
                return Convert.ToBase64String(encoding.GetBytes(text));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}