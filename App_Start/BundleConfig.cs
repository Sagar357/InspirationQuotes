﻿using System.Web;
using System.Web.Optimization;

namespace EverydayPower
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.4.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/luxon").Include(
                     "~/Scripts/luxon.js"));

            bundles.Add(new ScriptBundle("~/bundles/proper").Include(
          "~/Scripts/propper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
         "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/adapter").Include(
       "~/Scripts/adapters.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/styles/Custom-Post.css",
                       "~/ckeditor/plugins/dialog/styles/dialog.css",
                       "~/Content/normailze.css",
                       "~/Content/bootstrap-datepicker.css"));
        }
    }
}
