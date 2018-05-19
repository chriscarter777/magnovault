using System.Web;
using System.Web.Optimization;

namespace magnovault.Site
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {
               bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery.validate-vsdoc.js",
                         "~/Scripts/jquery.validate.js",
                         "~/Scripts/jquery.validate.unobtrusive.js",
                         "~/Scripts/jquery.unobtrusive-ajax.js",
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/respond.js"
                         ));

               bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/bootstrap.css",
                         "~/Content/Site.css"));
          }
     }
}
