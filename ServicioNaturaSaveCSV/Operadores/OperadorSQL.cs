using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioNaturaSaveCSV.Modelo;
using ServicioNaturaSaveCSV.Logs;

namespace ServicioNaturaSaveCSV.Operadores
{
    class OperadorSQL
    {
        private SP_ETL_CrearRegistroAuditoria_Result resultadoaRegistrarAuditoria = new SP_ETL_CrearRegistroAuditoria_Result();
        private SP_ETL_ActualizarRegistroAuditoria_Result resultadoaActualizarAuditoria = new SP_ETL_ActualizarRegistroAuditoria_Result();
        private List<SP_ETL_Obtener_Directorios_Result> listadoDirectoriosParam = new List<SP_ETL_Obtener_Directorios_Result>();
        private InfoNaturaEntities contextoModelo = new InfoNaturaEntities();
        private List<string> listadoRutasDirectorios = new List<string>();
        private string mensajeRegistroAuditoria = "";
        private EstadoAuditoriaSaveCSV estadoAuditoria = new EstadoAuditoriaSaveCSV();
        private int estadoParaRegistro = 0;
        private LogSaveNatura logProcesos = new LogSaveNatura();

        public string registrarAuditoria(string nombreArchivo, string rutaArchivo, string descripcionProceso, string estadoSolicitado, bool estadoDescarga, bool estadoCodificacion, bool estadoBulk)
        {
            try
            {
                estadoParaRegistro = contextoModelo.EstadoAuditoriaSaveCSV.Where(ESA => ESA.NombreEstado == estadoSolicitado).Select(ESA => ESA.IdEstado).FirstOrDefault();
                resultadoaRegistrarAuditoria = contextoModelo.SP_ETL_CrearRegistroAuditoria(nombreArchivo, rutaArchivo, descripcionProceso, estadoParaRegistro, estadoDescarga, estadoCodificacion, estadoBulk).FirstOrDefault();
                mensajeRegistroAuditoria = resultadoaRegistrarAuditoria.MensajeProceso;
            }
            catch (Exception ex)
            {
                mensajeRegistroAuditoria = "Error Generando Registro de Auditoria  - " + ex;
                logProcesos.registrarLog("HiloFTP - Proceso SQL", mensajeRegistroAuditoria, 0);
            }
            return mensajeRegistroAuditoria;
        }

        public string actualizarAuditoria(int idProceso, string nombreArchivo, string rutaArchivo, string descripcionProceso, string estadoSolicitado, bool estadoDescarga, bool estadoCodificacion, bool estadoBulk)
        {
            try
            {
                if (idProceso == 0)
                {
                    idProceso = contextoModelo.AuditoriaNaturaSaveCSV.Where(ANS => ANS.NombreArchivo == nombreArchivo && ANS.RutaArchivo == rutaArchivo).Select(ANS => ANS.IdProceso).FirstOrDefault();
                }

                estadoParaRegistro = contextoModelo.EstadoAuditoriaSaveCSV.Where(ESA => ESA.NombreEstado == estadoSolicitado).Select(ESA => ESA.IdEstado).FirstOrDefault();
                resultadoaActualizarAuditoria = contextoModelo.SP_ETL_ActualizarRegistroAuditoria(idProceso, nombreArchivo, rutaArchivo, descripcionProceso, estadoParaRegistro, estadoDescarga, estadoCodificacion, estadoBulk).FirstOrDefault();
                mensajeRegistroAuditoria = resultadoaActualizarAuditoria.MensajeProceso;

            }
            catch (Exception ex)
            {
                mensajeRegistroAuditoria = "Error Generando Aactualización de Auditoria  - " + ex;
                logProcesos.registrarLog("HiloFTP - Proceso SQL", mensajeRegistroAuditoria, 0);
            }
            return mensajeRegistroAuditoria;
        }

        public List<string> obtenerDirectoriosParam() {

            try
            {
                listadoDirectoriosParam = contextoModelo.SP_ETL_Obtener_Directorios().ToList();
                foreach (SP_ETL_Obtener_Directorios_Result directorioIndividual in listadoDirectoriosParam) {
                    listadoRutasDirectorios.Add(directorioIndividual.Carpeta);
                }
            }
            catch (Exception ex)
            {
                mensajeRegistroAuditoria = "Error Generando Listado de Directorios de FPARAM  - " + ex;
                logProcesos.registrarLog("HiloFTP - Proceso SQL", mensajeRegistroAuditoria, 0);
            }
            return listadoRutasDirectorios;
        }


    }
}
