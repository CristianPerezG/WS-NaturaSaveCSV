using System.Web;
using System.Web.Optimization;

namespace WebNaturaSaveCSV
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/Bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/popper.min.js",
                      "~/Scripts/jquery-3.3.1.slim.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                      "~/Scripts/DataTables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/SweetAlert").Include(
                      "~/Scripts/SweetAlert/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/Content/Home").Include(
                      "~/Scripts/Home/ProcesoPrincipal.js"));

            bundles.Add(new StyleBundle("~/Content/Styles").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/SweetAlert/sweetalert.css",
                      "~/Content/font-awesome.min.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            //// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));            
        }
    }
}
