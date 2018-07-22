using System.Web;
using System.Web.Optimization;

namespace magnovault.Site
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {
               bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                         "~/Scripts/jquery-{version}.min.js",
                         "~/Scripts/jquery.validate-vsdoc.js",
                         "~/Scripts/jquery.validate.min.js",
                         "~/Scripts/jquery.validate.unobtrusive.min.js",
                         "~/Scripts/jquery.unobtrusive-ajax.min.js",
                         "~/Scripts/bootstrap.min.js"
                         ));

               bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/bootstrap.min.css",
                         "~/Content/Site.css"));
          }
     }
}
