using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BudgetOnline.UI.Models.Alerts;
using BudgetOnline.UI.Views;

namespace BudgetOnline.Web.Infrastructure.Controls
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

            var sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append(render(item).ToHtmlString());
            }

            return new HtmlString(sb.ToString());
        }

        public static IHtmlString AlertGenerator(this HtmlHelper html)
        {
            if (HttpContext.Current.Request.QueryString["infoMessage"] == "created")
            {
                return new HtmlString(
                    AlertSuccess
                        .Render(new AlertSuccessModel
                                {
                                    Message = "Запись сохранена успешно (<a href='edit/" + HttpContext.Current.Request.QueryString["savedid"] + "'>ссылка</a>)",
                                    MessageSuffix = ""
                                }).ToHtmlString()
                        );
            }
            return null;
        }
    }
}
