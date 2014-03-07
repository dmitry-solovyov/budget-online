using System.Web.Http;
using System.Web.Mvc;
using BudgetOnline.Api.Infrastructure.Filters;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace BudgetOnline.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new UnhandledExceptionFilter());
            filters.Add(new AuthorizeAttribute());

            //filters.Add(new ForceHttpsAttribute());
        }
    }
}