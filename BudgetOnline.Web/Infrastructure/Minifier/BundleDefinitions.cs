using System.Web.Optimization;

namespace BudgetOnline.Web.Infrastructure.Minifier
{
    public static class BundleDefinitions
    {
        public static void EnableBootstrapBundle(this BundleCollection bundles)
        {
            var commonStylesBundle = new Bundle("~/content/css", new CssMinify());
            commonStylesBundle.Include("~/Content/bootstrap.min.css");
            commonStylesBundle.Include("~/Content/bootstrap-responsive.css");
            commonStylesBundle.Include("~/Content/site.css");
            commonStylesBundle.Include("~/Content/cus-icons.css");
            commonStylesBundle.Include("~/Content/bootstrap-select/bootstrap-select.min.css");
            commonStylesBundle.Include("~/Content/themes/flick/jquery-ui.css", new CssRewriteUrlTransform());
            commonStylesBundle.Include("~/Content/themes/flick/jquery.ui.theme.css", new CssRewriteUrlTransform());

            BundleTable.Bundles.Add(commonStylesBundle);


            var mainScriptsBundle = new Bundle("~/js", new JsMinify());
            mainScriptsBundle.Include("~/Scripts/jquery-2.1.0.min.js");

            BundleTable.Bundles.Add(mainScriptsBundle);


            var otherScriptsBundle = new Bundle("~/js2", new JsMinify());
            otherScriptsBundle.Include("~/Scripts/jquery-ui-1.10.4.min.js");
            otherScriptsBundle.Include("~/Scripts/modernizr-2.7.2.js");
            otherScriptsBundle.Include("~/Scripts/bootstrap.min.js");
            otherScriptsBundle.Include("~/Scripts/bootstrap-select/bootstrap-select.min.js");
            otherScriptsBundle.Include("~/Scripts/bootstrap-select/bootstrap-select.jquery.json");
            otherScriptsBundle.Include("~/Scripts/jquery.validate.js");
            otherScriptsBundle.Include("~/Scripts/jquery.postdatas-min.js");
            otherScriptsBundle.Include("~/Scripts/jsrender.min.js");
            otherScriptsBundle.Include("~/Scripts/jsrender.min.map");
            otherScriptsBundle.Include("~/Scripts/common.js");

            BundleTable.Bundles.Add(otherScriptsBundle);
        }
    }
}