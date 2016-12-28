using System.Web.Mvc;
using BudgetOnline.Web.Infrastructure.Attributes;
using BudgetOnline.Web.Infrastructure.Filters;

namespace BudgetOnline.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalExecuteActionFilterAttribute());
            filters.Add(new HandleViewErrorsAttribute());
            filters.Add(new LocalizationActionFilterAttribute());
        }
    }
}