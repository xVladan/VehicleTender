using System.Web;
using System.Web.Optimization;

namespace VehicleTender
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Inculiding DevExpress in project

            //bundles.Add(new ScriptBundle("~/bundles/dx.all").Include("~/Content/DX/js/dx.all.js"));
            //bundles.Add(new ScriptBundle("~/bundles/knockout").Include("~/Content/DX/js/knockout-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jszip.min").Include("~/Content/DX/js/jszip.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/globalize.min").Include("~/Content/DX/js/globalize.min.js"));
            //bundles.Add(new StyleBundle("~/bundles/dx.common").Include("~/Content/DX/css/dx.common.css"));
            //bundles.Add(new StyleBundle("~/bundles/dx.light").Include("~/Content/DX/css/dx.light.css"));
        }
    }
}
