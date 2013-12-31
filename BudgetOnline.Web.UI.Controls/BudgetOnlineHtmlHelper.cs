using System.Collections.Generic;
using System.Text;
using System.Web.WebPages;
using BudgetOnline.UI.Models.Alerts;
using BudgetOnline.Web.UI.Controls;

namespace System.Web.Mvc
{
	public static class BudgetOnlineHtmlHelper
	{
		public static BudgetOnlineWebControls BudgetOnlineWeb(this HtmlHelper htmlHelper)
		{
			return new BudgetOnlineWebControls();
		}

		public static IHtmlString Repeater<T>(this HtmlHelper html, IEnumerable<T> items, Func<T, HelperResult> render)
		{
			if (items == null)
				return new HtmlString("");

			int i = 0;
			var sb = new StringBuilder();
			foreach (var item in items)
			{
				sb.Append(render(item).ToHtmlString());
			}

			return new HtmlString(sb.ToString());
		}

		public static IHtmlString AlertGenerator(this HtmlHelper html)
		{
			if (HttpContext.Current.Request.QueryString["infoMessage"] == "record-was-saved")
			{
				return new HtmlString(
					new BudgetOnline.UI.Views.AlertSuccess()
						.Render(new AlertSuccessModel
								{
									Message = "Запись сохранена успешно",
									MessageSuffix = ""
								}).ToHtmlString()
						);
			}
			return null;
		}
	}
}
