using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace BudgetOnline.UI.Extensions
{
	public static class HtmlHelperExtensions
	{
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