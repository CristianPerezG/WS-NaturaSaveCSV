using ServicioNaturaSaveCSV.Logs;
using ServicioNaturaSaveCSV.Operadores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace ServicioNaturaSaveCSV.Operadores
{
    class OperadorWatcher
    {
        private readonly LogSaveNatura logProcesos = new LogSaveNatura();
        private readonly OperadorSQL operadorSQL = new OperadorSQL();
        private readonly string rutaRaizNatura = Properties.Settings.Default.RutaRaizNatura;
        private readonly string extencionArchivosFTP = Properties.Settings.Default.ExtensionArchivosFTP;
        private readonly string cadenaConexionSQL = Properties.Settings.Default.CadenaSQL;
        private readonly string inicioSPNatura = Properties.Settings.Default.InicioSPNatura;
        private string estadoDescargando = Properties.Settings.Default.EstadoDescargando;
        private string estadoCambiandoCod = Properties.Settings.Default.EstadoCambioandoCod;
        private string estadoBulk = Properties.Settings.Default.EstadoBulk;
        private string estadoFinalizado = Properties.Settings.Default.EstadoFinalizado;
        private string estadoFallido = Properties.Settings.Default.EstadoFallido;

        //private List<string> listaDirectoriosLocal;
        private List<FileSystemWatcher> watcherCarpetasFTP = new List<FileSystemWatcher>();
        private List<FileSystemWatcher> watcherCarpetasAPP = new List<FileSystemWatcher>();
        private FileSystemWatcher watcherIndividualCarpetas;
        private SqlConnection conexionSQL;
        private SqlCommand llamadoSP;

        private string resultadoProcesoInd = "", resultadoAuditoria = "", resultadoSPBULK = "";
        private int idProceso = 0;
        private SqlParameter rutaArchivo, idProcesosSQL;
        private string rutaBulkInsert;



        public void generarWatchersLocales(List<string> listaDirectoriosLocal)
        {
            try
            {
                logProcesos.registrarLog("OperadorWatcher", "Inicia proceso de Watcher de Directorios Locales.", 2);
                Console.WriteLine("Hilo inicia proceso de Watcher de Directorios.");

                //listaDirectoriosLocal = new string[Directory.GetDirectories(rutaRaizNatura).Length];
                //listaDirectoriosLocal = Directory.GetDirectories(rutaRaizNatura);

                foreach (string direcorioLocal in listaDirectoriosLocal)
                {
                    if (direcorioLocal.Contains("\\"))
                    {
                        watcherCarpetasAPP.Add(new FileSystemWatcher(direcorioLocal, "*" + extencionArchivosFTP));
                        Console.WriteLine("ConWatcher: -" + direcorioLocal);
                    }
                    else
                    {
                        watcherCarpetasFTP.Add(new FileSystemWatcher(rutaRaizNatura + direcorioLocal, "*" + extencionArchivosFTP));
                        Console.WriteLine("ConWatcher: -" + direcorioLocal);
                    }
                }

                foreach (FileSystemWatcher watcherDirectorio in watcherCarpetasFTP)
                {
                    watcherDirectorio.Renamed += new RenamedEventHandler(ayudaWatcherFTP);
                    watcherDirectorio.EnableRaisingEvents = true;
                }

                foreach (FileSystemWatcher watcherDirectorio in watcherCarpetasAPP)
                {
                    watcherDirectorio.Created += new FileSystemEventHandler(ayudaWatcherAPP);
                    watcherDirectorio.EnableRaisingEvents = true;
                }

                logProcesos.registrarLog("OperadorWatcher", "Finaliza Proceso de Carga de Watchers en Directorios Locales de manera Correcta.", 2);
                Console.WriteLine("Finaliza Proceso de Carga de Watchers en Directorios Locales de manera Correcta.");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                logProcesos.registrarLog("OperadorWatcher", "Error Generando WATCHERS en directorio LOCAL " + ex, 0);
                Console.WriteLine("Error Generando WATCHERS en directorio LOCAL " + ex);
            }
        }

        private void ayudaWatcherFTP(object source, RenamedEventArgs e)
        {
            cargarBulkInsertFTP(inicioSPNatura + Path.GetFileName(e.FullPath.Replace("\\" + e.Name, "")), e);
        }


        private void ayudaWatcherAPP(object source, FileSystemEventArgs e)
        {
            cargarBulkInsertAPP(inicioSPNatura + Path.GetFileName(e.FullPath.Replace("\\" + e.Name, "")), e);
        }

        private void cargarBulkInsertFTP(string spSQL, FileSystemEventArgs e)
        {
            try
            {
                rutaBulkInsert = e.FullPath;
                logProcesos.registrarLog("OperadorWatcher", "Inicia el BULKINSERT del archivo: " + Path.GetFileNameWithoutExtension(rutaBulkInsert), 2);
                operadorSQL.actualizarAuditoria(0, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Insertando el Archivo en BULK INSERT SQL", estadoBulk, true, true, true);
                idProceso = 0;

                conexionSQL = new SqlConnection(@cadenaConexionSQL);
                llamadoSP = new SqlCommand(spSQL, conexionSQL);
                llamadoSP.CommandType = CommandType.StoredProcedure;
                rutaArchivo = new SqlParameter("@rutaArchivo", rutaBulkInsert);
                idProcesosSQL = new SqlParameter("@idLogAuditoria", idProceso);
                llamadoSP.Parameters.Add(idProcesosSQL);
                llamadoSP.Parameters.Add(rutaArchivo);
                conexionSQL.Open();
                SqlDataReader reader = llamadoSP.ExecuteReader();

                while (reader.Read())
                {
                    resultadoSPBULK = reader["MensajeProceso"].ToString();
                }

                if (resultadoSPBULK.Contains("Error") || resultadoSPBULK.Contains("error"))
                {
                    logProcesos.registrarLog("OperadorWatcher", "Error realizando el BULK del Archivo: " + rutaBulkInsert + " -- " + resultadoSPBULK, 0);
                    logProcesos.registrarLog("OperadorWatcher", "Error realizando el BULK del Archivo: " + rutaBulkInsert + " -- " + resultadoSPBULK + " - Finaliza Conexion con SQL", 2);
                    operadorSQL.actualizarAuditoria(0, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Error realizando el BULK del Archivo: " + rutaBulkInsert + " -- " + resultadoSPBULK, estadoFallido, true, true, false);
                }
                else
                {
                    Console.WriteLine("Finaliza el BULKINSERT del archivo: " + Path.GetFileNameWithoutExtension(rutaBulkInsert) + " - Finaliza Conexion con SQL");
                    operadorSQL.actualizarAuditoria(0, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Proceso del Archivo realizado Correctamente en su Totalidad.", estadoFinalizado, true, true, true);
                    logProcesos.registrarLog("OperadorWatcher", "Finaliza el BULKINSERT del archivo: " + Path.GetFileNameWithoutExtension(rutaBulkInsert) + " - Finaliza Conexion con SQL", 2);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("An unexpected end of file was encountered in the data file."))
                {
                    logProcesos.registrarLog("OperadorWatcher", "Error de ESTRUCTURA en el archivo: " + rutaBulkInsert, 0);
                    operadorSQL.actualizarAuditoria(0, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Error realizando BULK INSERT del Archivo - Error de estructura del archivo.", estadoFallido, true, true, false);
                }
                else
                {
                    logProcesos.registrarLog("OperadorWatcher", "Error Generando BULKINSERT del archivo: " + rutaBulkInsert + "   " + ex, 0);
                    operadorSQL.actualizarAuditoria(0, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Error Generando BULKINSERT del archivo: " + ex, estadoFallido, true, true, false);
                }
                Console.WriteLine("Error Generando BULKINSERT: \n" + ex);
            }
            finally
            {
                conexionSQL.Close();
            }
        }

        private void cargarBulkInsertAPP(string spSQL, FileSystemEventArgs e)
        {
            try
            {
                rutaBulkInsert = e.FullPath;
                logProcesos.registrarLog("OperadorWatcher", "Inicia el BULKINSERT del archivo: " + Path.GetFileNameWithoutExtension(rutaBulkInsert), 2);

                resultadoAuditoria = operadorSQL.registrarAuditoria(Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Insertando el Archivo en BULK INSERT SQL", estadoBulk, true, true, true);
                idProceso = int.Parse(resultadoAuditoria.Substring(resultadoAuditoria.IndexOf("[") + 1, 1));

                conexionSQL = new SqlConnection(@cadenaConexionSQL);
                llamadoSP = new SqlCommand(spSQL, conexionSQL);
                llamadoSP.CommandType = CommandType.StoredProcedure;
                rutaArchivo = new SqlParameter("@rutaArchivo", rutaBulkInsert);
                idProcesosSQL = new SqlParameter("@idLogAuditoria", idProceso);
                llamadoSP.Parameters.Add(idProcesosSQL);
                llamadoSP.Parameters.Add(rutaArchivo);
                conexionSQL.Open();
                SqlDataReader reader = llamadoSP.ExecuteReader();
                while (reader.Read())
                {
                    resultadoSPBULK = reader["MensajeProceso"].ToString();
                }
                conexionSQL.Close();


                if (resultadoSPBULK.Contains("Error") || resultadoSPBULK.Contains("error"))
                {
                    logProcesos.registrarLog("OperadorWatcher", "Error realizando el BULK del Archivo: " + rutaBulkInsert + " -- " + resultadoSPBULK, 0);
                    logProcesos.registrarLog("OperadorWatcher", "Error realizando el BULK del Archivo: " + rutaBulkInsert + " -- " + resultadoSPBULK + " - Finaliza Conexion con SQL", 2);
                    operadorSQL.actualizarAuditoria(idProceso, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Error realizando el BULK del Archivo: " + rutaBulkInsert + " -- " + resultadoSPBULK, estadoFallido, true, true, false);
                }
                else
                {
                    Console.WriteLine("Finaliza el BULKINSERT del archivo: " + Path.GetFileNameWithoutExtension(rutaBulkInsert) + " - Finaliza Conexion con SQL");
                    operadorSQL.actualizarAuditoria(idProceso, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Proceso del Archivo realizado Correctamente en su Totalidad.", estadoFinalizado, true, true, true);
                    logProcesos.registrarLog("OperadorWatcher", "Finaliza el BULKINSERT del archivo: " + Path.GetFileNameWithoutExtension(rutaBulkInsert) + " - Finaliza Conexion con SQL", 2);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("An unexpected end of file was encountered in the data file."))
                {
                    logProcesos.registrarLog("OperadorWatcher", "Error de ESTRUCTURA en el archivo: " + rutaBulkInsert, 0);
                    operadorSQL.actualizarAuditoria(idProceso, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Error realizando BULK INSERT del Archivo - Error de estructura del archivo.", estadoFallido, true, true, false);
                }
                else
                {
                    logProcesos.registrarLog("OperadorWatcher", "Error Generando BULKINSERT del archivo: " + rutaBulkInsert + "   " + ex, 0);
                    operadorSQL.actualizarAuditoria(idProceso, Path.GetFileName(rutaBulkInsert), Path.GetFileName(Path.GetDirectoryName(rutaBulkInsert)) + "/" + Path.GetFileName(rutaBulkInsert), "Error Generando BULKINSERT del archivo: " + ex, estadoFallido, true, true, false);
                }
                Console.WriteLine("Error Generando BULKINSERT: \n" + ex);
            }
        }

        public string generarWatcherDirectorio(string directorioSolicitado)
        {
            try
            {
                if (directorioSolicitado.Contains("\\"))
                {
                    watcherIndividualCarpetas = new FileSystemWatcher(directorioSolicitado, "*" + extencionArchivosFTP);
                    watcherIndividualCarpetas.Created += new FileSystemEventHandler(ayudaWatcherAPP);
                    watcherIndividualCarpetas.EnableRaisingEvents = true;
                }
                else
                {
                    watcherIndividualCarpetas = new FileSystemWatcher(rutaRaizNatura + directorioSolicitado, "*" + extencionArchivosFTP);
                    watcherIndividualCarpetas.Renamed += new RenamedEventHandler(ayudaWatcherFTP);
                    watcherIndividualCarpetas.EnableRaisingEvents = true;
                }
                resultadoProcesoInd = "Proceso de carga de Watcher para el directorio " + rutaRaizNatura + directorioSolicitado + " se ha realizado de manera correcta.";
                logProcesos.registrarLog("OperadorWatcher", resultadoProcesoInd, 2);
                Console.WriteLine(resultadoProcesoInd);
            }
            catch (Exception ex)
            {
                resultadoProcesoInd = "Error Generando WATCHERS en directorio LOCAL Individual " + ex;
                logProcesos.registrarLog("OperadorWatcher", resultadoProcesoInd, 0);
                Console.WriteLine(resultadoProcesoInd);
            }
            return resultadoProcesoInd;
        }
    }
}
