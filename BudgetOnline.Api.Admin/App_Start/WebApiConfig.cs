using System.Web.Http;

namespace BudgetOnline.Api.Admin
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.Clear();

            config.MapHttpAttributeRoutes();
        }
    }
}
