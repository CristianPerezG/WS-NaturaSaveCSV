using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNaturaSaveCSV.Models;

namespace WebNaturaSaveCSV.Util
{
    public class UtilSQL
    {
        private readonly InfoNaturaEntities contextoModelo = new InfoNaturaEntities();        
        private List<SP_ETL_ReporteAuditoria_Result> resultadoReporteAuditoria = new List<SP_ETL_ReporteAuditoria_Result>();
        //private EstadoAuditoriaSaveCSV estadosAuditoria = new EstadoAuditoriaSaveCSV();
        
        public List<SP_ETL_ReporteAuditoria_Result> geneararReporteAuditoria(string nombreArcihvo, string rutaArchivo, Nullable<DateTime> fechaInicial, Nullable<DateTime> fechaUltimaModificacicon, string descripcionProceso, Nullable<Int32> estadoProceso, Nullable<Boolean> estadoDescarga, Nullable<Boolean> estadoCodificaicon, Nullable<Boolean> estadoBulk) {
            resultadoReporteAuditoria = contextoModelo.SP_ETL_ReporteAuditoria(nombreArcihvo, rutaArchivo, fechaInicial, fechaUltimaModificacicon, descripcionProceso, estadoProceso, estadoDescarga, estadoCodificaicon, estadoBulk).ToList();
            return resultadoReporteAuditoria;
        }

    }
}