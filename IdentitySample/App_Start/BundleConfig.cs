using System.Globalization;
using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/countdown").Include(
                        "~/Scripts/jquery.countdown.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/kb_display").Include(
                        "~/Scripts/kb_display.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/sms").Include(
                        "~/Scripts/sms.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalization")
                    .Include("~/Scripts/jquery.globalize/globalize.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/bootstrap-datepicker-globalize.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/PagedList.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css_display").Include(
                     "~/Content/bootstrap.css",    
                     "~/Content/DisplayFullHD.css"));
        }
    }
}
