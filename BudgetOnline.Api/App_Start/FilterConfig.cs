using System.Web.Http;
using System.Web.Http.Filters;
using BudgetOnline.Api.Infrastructure.Filters;

namespace BudgetOnline.Api
{
    public static class FilterConfig
    {
        public static IFilter[] GlobalFilters()
        {
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());

            //filters.Add(new ForceHttpsAttribute());

            return new IFilter[0];
        }
    }
}