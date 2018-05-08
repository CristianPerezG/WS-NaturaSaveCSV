using ServicioNaturaSaveCSV.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using ServicioNaturaSaveCSV.Operadores;


namespace ServicioNaturaSaveCSV.Operadores
{
    class OperadorArchivos
    {
        private FtpWebRequest solicitudFTP;
        private FtpWebResponse resupuestaFTP;
        private List<string> listadoArchivosFTP = new List<string>();
        private Stream streamResultados;
        private Stream streamArchivo;
        private StreamReader lectorStramsResultados;
        private string rutaLocalAlmacenaje, resultadoProceso, resultadoAuditoria;
        private readonly LogSaveNatura logProcesos = new LogSaveNatura();
        private readonly string rutaRaizNatura = Properties.Settings.Default.RutaRaizNatura;
        private readonly NetworkCredential credencialesFTP = new NetworkCredential(Properties.Settings.Default.UsuarioFTP, Properties.Settings.Default.PasswrodFTP);
        private readonly string servidorFTP = Properties.Settings.Default.HostFTP;
        private readonly string extencionArchivosFTP = Properties.Settings.Default.ExtensionArchivosFTP;
        private readonly string extencionArchivosLocales = Properties.Settings.Default.ExtensionArchivosLocal;
        private readonly OperadorSQL operadorSQL = new OperadorSQL();
        private readonly OperadorWatcher operadorWatcher = new OperadorWatcher();
        private string estadoDescargando = Properties.Settings.Default.EstadoDescargando;
        private string estadoCambiandoCod = Properties.Settings.Default.EstadoCambioandoCod;
        private string estadoBulk = Properties.Settings.Default.EstadoBulk;
        private string estadoFinalizado = Properties.Settings.Default.EstadoFinalizado;
        private string estadoFallido = Properties.Settings.Default.EstadoFallido;
        private int idProceso;
        private byte[] bufferArchivo;

        public string descargarArchivo(string nombreArchivo, string nombreCarpeta)
        {
            try
            {
                resultadoAuditoria = operadorSQL.registrarAuditoria(nombreArchivo, nombreCarpeta + "/" + nombreArchivo, "Descargando el archivo del FTP", estadoDescargando, true, false, false);
                idProceso = int.Parse(resultadoAuditoria.Substring(resultadoAuditoria.IndexOf("[") + 1, 1));

                solicitudFTP = (FtpWebRequest)WebRequest.Create(servidorFTP + nombreCarpeta + "/" + nombreArchivo);
                solicitudFTP.UseBinary = true;
                solicitudFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                solicitudFTP.Credentials = credencialesFTP;
                solicitudFTP.KeepAlive = false;
                solicitudFTP.UsePassive = false;

                Console.WriteLine("Inicia descarga de archivo por FTP " + nombreArchivo);
                resupuestaFTP = (FtpWebResponse)solicitudFTP.GetResponse();
                Console.WriteLine("Finaliza descarga de archivo por FTP " + nombreArchivo);

                streamResultados = resupuestaFTP.GetResponseStream();
                rutaLocalAlmacenaje = Path.Combine(rutaRaizNatura + nombreCarpeta, nombreArchivo.Replace(extencionArchivosFTP, extencionArchivosLocales));

                if (File.Exists(rutaLocalAlmacenaje))
                {
                    File.Delete(rutaLocalAlmacenaje);
                    streamArchivo = File.Create(rutaLocalAlmacenaje);
                    int lectorArchivo;
                    bufferArchivo = new byte[1024];
                    while ((lectorArchivo = streamResultados.Read(bufferArchivo, 0, bufferArchivo.Length)) > 0)
                    {
                        streamArchivo.Write(bufferArchivo, 0, lectorArchivo);
                    }
                    streamArchivo.Close();
                }
                else
                {
                    streamArchivo = File.Create(rutaLocalAlmacenaje);
                    int lectorArchivo;
                    bufferArchivo = new byte[1024];
                    while ((lectorArchivo = streamResultados.Read(bufferArchivo, 0, bufferArchivo.Length)) > 0)
                    {
                        streamArchivo.Write(bufferArchivo, 0, lectorArchivo);
                    }
                    streamArchivo.Close();                    
                }         

                if (Path.GetExtension(nombreArchivo).Equals(extencionArchivosFTP))
                {
                    operadorSQL.actualizarAuditoria(idProceso, nombreArchivo, nombreCarpeta + "/" + nombreArchivo, "Cambiando Codificación del Archivo a UNICODE", estadoCambiandoCod, true, true, false);
                    File.WriteAllText(rutaLocalAlmacenaje, File.ReadAllText(rutaLocalAlmacenaje, Encoding.UTF8), Encoding.BigEndianUnicode);
                }
                else
                {
                    operadorSQL.actualizarAuditoria(idProceso, nombreArchivo, nombreCarpeta + "/" + nombreArchivo, "NO SE REALIZA el Cambio de Codificación del Archivo a UNICODE, ya que no es un archivo CSV", estadoCambiandoCod, true, true, false);
                }

                File.Move(rutaLocalAlmacenaje, rutaLocalAlmacenaje.Replace(extencionArchivosLocales, extencionArchivosFTP));
                resultadoProceso = "Archivo descargado Exitosamente en la ruta: " + rutaRaizNatura + nombreCarpeta + "\\" + nombreArchivo;
                Console.WriteLine(resultadoProceso);
            }
            catch (Exception ex)
            {
                resultadoProceso = "Error Descargando Archivos en Local desde FTP o el el proceso de Codificacion o Modificacion de Archivos  - " + ex;
                Console.WriteLine(resultadoProceso);
                logProcesos.registrarLog("OperadorArchivos", resultadoProceso, 0);
                operadorSQL.actualizarAuditoria(idProceso, nombreArchivo, nombreCarpeta + "/" + nombreArchivo, resultadoProceso, estadoFallido, true, true, false);
            }
            finally
            {
                if (lectorStramsResultados != null)
                {
                    lectorStramsResultados.Close();
                }
                if (streamResultados != null)
                {
                    streamResultados.Close();
                }
            }

            return resultadoProceso;
        }

        public string eliminarArchivo(string nombreArchivo, string nombreCarpeta)
        {
            try
            {
                solicitudFTP = (FtpWebRequest)WebRequest.Create(servidorFTP + nombreCarpeta + "/" + nombreArchivo);
                solicitudFTP.UseBinary = true;
                solicitudFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                solicitudFTP.Credentials = credencialesFTP;
                solicitudFTP.KeepAlive = false;
                solicitudFTP.UsePassive = false;

                resupuestaFTP = (FtpWebResponse)solicitudFTP.GetResponse();
                streamResultados = resupuestaFTP.GetResponseStream();
                lectorStramsResultados = new StreamReader(streamResultados);

                resultadoProceso = "Archivo Eliminado de Manera Exitosa en la ruta: " + servidorFTP + nombreCarpeta + "/" + nombreArchivo;
                Console.WriteLine(resultadoProceso);
                logProcesos.registrarLog("OperadorArchivos", resultadoProceso, 1);
            }
            catch (Exception ex)
            {
                resultadoProceso = "Error Eliminado Archivosde FTP - " + ex;
                Console.WriteLine(resultadoProceso);
                logProcesos.registrarLog("OperadorArchivos", resultadoProceso, 0);
                operadorSQL.actualizarAuditoria(idProceso, nombreArchivo, nombreCarpeta + "/" + nombreArchivo, resultadoProceso, estadoFallido, true, true, false);
            }
            finally
            {
                if (lectorStramsResultados != null)
                {
                    lectorStramsResultados.Close();
                }
                if (streamResultados != null)
                {
                    streamResultados.Close();
                }
            }
            return resultadoProceso;
        }
    }
}
