using System.Web.Optimization;

namespace BudgetOnline.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap.min.css", 
                "~/Content/bootstrap-responsive.css",
                "~/Content/bootstrap-select.min.css", 
                "~/Content/cus-icons.css",
                "~/Content/themes/flick/jquery-ui.css", 
                "~/Content/themes/flick/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui-i18n.min.js"));

            bundles.Add(new ScriptBundle("~/highcharts")
                .Include("~/Scripts/Highcharts-4.0.1/js/highcharts-all.js"));

            bundles.Add(new ScriptBundle("~/js2").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.postdatas-min.js",
                "~/Scripts/jquery.cookie-1.4.1.min.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-select.min.js",
                "~/Scripts/i18n/defaults-ru_RU.min.js",
                "~/Scripts/i18n/defaults-en_US.min.js",
                "~/Scripts/jsrender.min.js", 
                "~/Scripts/common.js", 
                "~/Scripts/moment.min.js"));

        }
    }
}