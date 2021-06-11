using System.Web.Optimization;

namespace ReservationFinal.MVC.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/styles.css",
                      "~/Content/custom.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                
                ));
        }
    }
}
