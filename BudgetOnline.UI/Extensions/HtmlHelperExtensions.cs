using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using BudgetOnline.UI.Helpers;

namespace System.Web.Mvc
{
	public static class HtmlHelperExtensions
	{
		private static readonly Random Random = new Random();

		public static string GetUniqId(this HtmlHelper helper, int length)
		{
			return UIHelper.GenerateRandomCode(length);
		}

		public static string GetUrlQithNewQueryParameter(this HtmlHelper helper, string key, string value)
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

		public static string RenderRazorViewToString(ControllerContext controllerContext, string viewPath, object model = null)
		{
			using (var sw = new StringWriter())
			{
				if (model != null)
					controllerContext.Controller.ViewData.Model = model;

				var viewContext = new ViewContext(
					controllerContext,
					new RazorView(controllerContext, viewPath, null, false, null, null),
					controllerContext.Controller.ViewData,
					controllerContext.Controller.TempData,
					sw);

				var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewPath);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);

				return sw.GetStringBuilder().ToString();
			}
		}

		public static string RenderPartialToString(string controlName, object viewData)
		{
			var viewContext = new ViewContext();
			var urlHelper = new UrlHelper(viewContext.RequestContext);
			var viewDataDictionary = new ViewDataDictionary(viewData);

			var viewPage = new ViewPage
			{
				ViewData = viewDataDictionary,
				ViewContext = viewContext,
				Url = urlHelper
			};

			var control = viewPage.LoadControl(controlName);
			viewPage.Controls.Add(control);

			var sb = new StringBuilder();
			using (var sw = new StringWriter(sb))
			using (var tw = new HtmlTextWriter(sw))
			{
				viewPage.RenderControl(tw);
			}

			return sb.ToString();
		}
	}
}