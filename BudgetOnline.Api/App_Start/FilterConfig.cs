﻿using System.Web.Http.Filters;

namespace BudgetOnline.Api
{
    public static class FilterConfig
    {
        public static IFilter[] GlobalFilters()
        {
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());
            //filters.Add(new ForceHttpsAttribute());

            return new IFilter[]
            {
                //new RequestAuthorizeAttribute()
            };
        }
    }
}