using System.Web.Optimization;

namespace BudgetOnline.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.min.css", 
                "~/Content/bootstrap-responsive.css", 
                "~/Content/site.css", 
                "~/Content/cus-icons.css",
                "~/Content/themes/flick/jquery-ui.css", 
                "~/Content/themes/flick/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/highcharts")
                .Include("~/Scripts/Highcharts-4.0.1/js/highcharts-all.js"));

            bundles.Add(new ScriptBundle("~/js2").Include(
                "~/Scripts/jquery-ui-{version}.js", 
                "~/Scripts/jquery-ui-i18n.min.js", 
                "~/Scripts/modernizr-*", 
                "~/Scripts/bootstrap.min.js", 
                "~/Scripts/bootstrap-select/bootstrap-select.js",
                "~/Scripts/jquery.validate*", 
                "~/Scripts/jquery.postdatas-min.js", 
                "~/Scripts/jsrender.min.js", 
                "~/Scripts/common.js", 
                "~/Scripts/moment.min.js"));

        }
    }
}