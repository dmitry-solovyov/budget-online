using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace BudgetOnline.Web.Infrastructure.Filters
{
    public class LocalizationActionFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var lang = filterContext.RequestContext.HttpContext.Request.Cookies.Get("lang");
            if (lang != null && !string.IsNullOrWhiteSpace(lang.Value))
            {
                var cultuer = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(x => x.Name == lang.Value);
                if (cultuer != null)
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = cultuer;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}