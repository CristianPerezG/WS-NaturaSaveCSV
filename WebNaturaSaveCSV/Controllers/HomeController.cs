using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNaturaSaveCSV.Models;
using WebNaturaSaveCSV.Util;

namespace WebNaturaSaveCSV.Controllers
{
    public class HomeController : Controller
    {

        private List<SP_ETL_ReporteAuditoria_Result> resultadoReporteAuditoria = new List<SP_ETL_ReporteAuditoria_Result>();
        private UtilSQL utilSQL = new UtilSQL();

        public HomeController()
        {
            resultadoReporteAuditoria = utilSQL.geneararReporteAuditoria(null, null, null, null, null, null, null, null, null);
        }


        public ActionResult Index()
        {
            return View(resultadoReporteAuditoria);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}