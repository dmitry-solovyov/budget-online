namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string Api(this UrlHelper helper, string controller, string id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                return string.Format("{0}api/{1}", helper.Content("~"), controller);

            return string.Format("{0}api/{1}/{2}", helper.Content("~"), controller, id);
        }
    }
}