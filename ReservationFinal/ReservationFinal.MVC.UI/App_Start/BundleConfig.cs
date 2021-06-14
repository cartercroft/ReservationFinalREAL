using System.Web.Optimization;

namespace ReservationFinal.MVC.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/styles.css",
                      "~/Content/custom.css",
                      "~/Content/css/jquery.dataTables.min.css",
                      "~/Content/css/responsive.dataTables.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/js/bootstrap.bundle.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/DataTables.js").Include(
                "~/Scripts/js/jquery-ui.js",
                "~/Scripts/js/jquery.dataTables.min.js",
                "~/Scripts/js/dataTables.responsive.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Ajax.js").Include(
                "~/Scripts/js/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/js/jquery-ui.js"
                ));
        }
    }
}
