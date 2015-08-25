using System.Web.Optimization;

namespace BudgetOnline.Web.Infrastructure.Minifier
{
    public static class BundleDefinitions
    {
        public static void EnableBootstrapBundle(this BundleCollection bundles)
        {
            var commonStylesBundle = new Bundle("~/content/css", new CssMinify());
            commonStylesBundle.Include("~/Content/bootstrap.min.css");
            commonStylesBundle.Include("~/Content/bootstrap-select.min.css");
            commonStylesBundle.Include("~/Content/site.css");
            commonStylesBundle.Include("~/Content/cus-icons.css");
            commonStylesBundle.Include("~/Content/bootstrap-select/bootstrap-select.css");
            commonStylesBundle.Include("~/Content/themes/flick/jquery-ui.css", new CssRewriteUrlTransform());
            commonStylesBundle.Include("~/Content/themes/flick/jquery.ui.theme.css", new CssRewriteUrlTransform());
            commonStylesBundle.Include("~/Content/angular-ui.css");
            commonStylesBundle.Include("~/Content/font-awesome.min.css");

            BundleTable.Bundles.Add(commonStylesBundle);


            var mainScriptsBundle = new Bundle("~/js", new JsMinify());
            mainScriptsBundle.Include("~/Scripts/jquery-{version}.js");
            mainScriptsBundle.Include("~/Scripts/angular.min.js");

            BundleTable.Bundles.Add(mainScriptsBundle);


            var angularAppBundle = new Bundle("~/angularApp", new JsMinify());
            angularAppBundle.Include("~/app/app.js");
            angularAppBundle.Include("~/app/common/common.js");
            angularAppBundle.Include("~/app/common/logger.js");

            angularAppBundle.Include("~/app/rightbar/rightbar.js");

            angularAppBundle.Include("~/Scripts/angular-route.js");
            angularAppBundle.Include("~/Scripts/angular-ui.min.js");
            angularAppBundle.Include("~/Scripts/angular-route.min.js");
            angularAppBundle.Include("~/Scripts/angular-animate.min.js");
            angularAppBundle.Include("~/Scripts/angular-sanitize.min.js");

            angularAppBundle.Include("~/Scripts/angular-ui/ui-bootstrap.min.js");
            angularAppBundle.Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js");
            angularAppBundle.Include("~/Scripts/angular-ui/ui-utils.min.js");

            BundleTable.Bundles.Add(angularAppBundle);


            var otherScriptsBundle = new Bundle("~/js2", new JsMinify());
            otherScriptsBundle.Include("~/Scripts/jquery-ui-{version}.js");
            otherScriptsBundle.Include("~/Scripts/jquery-ui-i18n.min.js");
            otherScriptsBundle.Include("~/Scripts/modernizr-{version}.js");
            otherScriptsBundle.Include("~/Scripts/bootstrap.min.js");
            otherScriptsBundle.Include("~/Scripts/bootstrap-select/bootstrap-select.js");
            otherScriptsBundle.Include("~/Scripts/jquery.validate.js");
            otherScriptsBundle.Include("~/Scripts/jquery.postdatas-min.js");
            otherScriptsBundle.Include("~/Scripts/jsrender.min.js");
            otherScriptsBundle.Include("~/Scripts/common.js");
            otherScriptsBundle.Include("~/Scripts/moment.min.js");

            BundleTable.Bundles.Add(otherScriptsBundle);
        }
    }
}