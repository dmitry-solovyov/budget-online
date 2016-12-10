using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BudgetOnline.UI.Helpers
{
	public static class UIHelper
	{
		private static readonly Random Random = new Random();

		public static string GenerateRandomCode(int length)
		{
			const string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
			var sb = new StringBuilder();

			for (int i = 0; i < length; i++)
			{
				sb.Append(charPool[Random.Next(charPool.Length - 1)]);
			}

			return sb.ToString();
		}

        public static string GetUrlQithNewQueryParameter(string key, string value)
        {
            if (!HttpContext.Current.Request.QueryString.HasKeys())
                return HttpContext.Current.Request.Url + string.Format("?{0}={1}", key, value);

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString[key]))
            {
                var query = new Dictionary<string, object>();
                HttpContext.Current.Request.QueryString.CopyTo(query);
                query[key] = value;

                return HttpContext.Current.Request.Path + "?" + string.Join("&", query.Select(o => string.Format("{0}={1}", o.Key, o.Value as string)));
            }

            return HttpUtility.UrlPathEncode(HttpContext.Current.Request.Url + string.Format("&{0}={1}", key, value));
        }
	}
}