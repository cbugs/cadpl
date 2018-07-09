﻿using System.Web.Optimization;

namespace Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui/jquery-ui.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
                "~/admin-lte/js/app.js",
                "~/admin-lte/plugins/fastclick/fastclick.js",
                "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jtable").Include(
                "~/Scripts/jtable/jquery.jtable.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/admin-lte/css/AdminLTE.css",
                "~/admin-lte/css/skins/skin-blue.css",
                "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                "~/Content/font-awesome.css",
                "~/Content/Ionicons/css/ionicons.css",
                "~/Scripts/jquery-ui/jquery-ui.min.css",
                "~/Scripts/jtable/themes/lightcolor/blue/jtable.min.css"
          ));
        }
    }
}