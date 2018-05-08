using ServicioNaturaSaveCSV.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ServicioNaturaSaveCSV.Operadores
{
    class OperadorDirectorios
    {
        private FtpWebRequest solicitudFTP;
        private FtpWebResponse resupuestaFTP;
        private List<string> listadoDirectoriosFTP;
        private Stream streamResultados;
        private StreamReader lectorStramsResultados;
        private readonly NetworkCredential credencialesFTP = new NetworkCredential(ServicioNaturaSaveCSV.Properties.Settings.Default.UsuarioFTP, ServicioNaturaSaveCSV.Properties.Settings.Default.PasswrodFTP);
        private string lineaFTPResultado,nombreDirectorio, rutaLocalDirectorio, resultadoVerificacion = "", resultadoDescarga;
        private readonly string rutaRaizNatura = ServicioNaturaSaveCSV.Properties.Settings.Default.RutaRaizNatura;
        private LogSaveNatura logProcesos = new LogSaveNatura();
        private OperadorArchivos operadorArchivos = new OperadorArchivos();
        private readonly string servidorFTP = ServicioNaturaSaveCSV.Properties.Settings.Default.HostFTP;

        public List<string> verificarEstructuraDirectorios()
        {
            try
            {
                listadoDirectoriosFTP = new List<string>();
                solicitudFTP = (FtpWebRequest)WebRequest.Create(servidorFTP);
                solicitudFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                solicitudFTP.Credentials = credencialesFTP;
                resupuestaFTP = (FtpWebResponse)solicitudFTP.GetResponse();
                streamResultados = resupuestaFTP.GetResponseStream();
                lectorStramsResultados = new StreamReader(streamResultados);

                logProcesos.registrarLog("OperadorDirectorios", "Listado de Directorios Obtenidos de manera Correcta", 1);
                Console.WriteLine("Listado de Directorios Obtenidos de manera Correcta");
                Console.WriteLine("");

                while (!lectorStramsResultados.EndOfStream)
                {
                    lineaFTPResultado = lectorStramsResultados.ReadLine();       
                    nombreDirectorio = lineaFTPResultado.Substring(55);
                    rutaLocalDirectorio = Path.Combine(rutaRaizNatura, nombreDirectorio);
                    if (!Directory.Exists(rutaLocalDirectorio))
                    {
                        Directory.CreateDirectory(rutaLocalDirectorio);
                        resultadoVerificacion = "Existe Algun Directorio en FTP que no esta en LOCAL - " + nombreDirectorio;
                    }
                    listadoDirectoriosFTP.Add(nombreDirectorio);
                    Console.WriteLine(rutaLocalDirectorio);
                }

                Console.WriteLine("");

                if (resultadoVerificacion.Equals(""))
                {
                    logProcesos.registrarLog("OperadorDirectorios", "Estructuras iguales en FTP y LOCAL.", 1);
                    Console.WriteLine("Estructuras iguales en FTP y LOCAL.");
                    Console.WriteLine("");
                }
                else
                {
                    logProcesos.registrarLog("OperadorDirectorios", resultadoVerificacion, 1);
                    Console.WriteLine(resultadoVerificacion);
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                logProcesos.registrarLog("OperadorDirectorios", "Error Generando Listado de Directorios FTP: " + ex, 0);
                Console.WriteLine("Error Generando Listado de Directorios" + ex);
            }
            finally
            {
                lectorStramsResultados.Close();
                streamResultados.Close();
            }
            return listadoDirectoriosFTP;
        }

        public void listarArchivosDirectorio(List<string> listadoDirectoriosFTPO)
        {
            try
            {
                foreach (string directorioFTP in listadoDirectoriosFTPO)
                {
                    if (!directorioFTP.Contains("\\"))
                    {
                        solicitudFTP = (FtpWebRequest)WebRequest.Create(servidorFTP + directorioFTP);
                        solicitudFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                        solicitudFTP.Credentials = credencialesFTP;
                        resupuestaFTP = (FtpWebResponse)solicitudFTP.GetResponse();
                        streamResultados = resupuestaFTP.GetResponseStream();
                        lectorStramsResultados = new StreamReader(streamResultados);

                        Console.WriteLine("Listando Archivos del Directorio: " + directorioFTP);
                        logProcesos.registrarLog("OperadorDirectorios", "Listando Archivos del Directorio: " + directorioFTP, 1);

                        while (!lectorStramsResultados.EndOfStream)
                        {
                            lineaFTPResultado = lectorStramsResultados.ReadLine();                            
                            nombreDirectorio = lineaFTPResultado.Substring(55);

                            resultadoDescarga = operadorArchivos.descargarArchivo(nombreDirectorio, directorioFTP);

                            if (!resultadoDescarga.Contains("Error"))
                            {
                                operadorArchivos.eliminarArchivo(nombreDirectorio, directorioFTP);
                            }
                        }

                        logProcesos.registrarLog("OperadorDirectorios", "Proceso Finalizado en el Directorio: " + directorioFTP, 1);
                        Console.WriteLine("Proceso Finalizado en el Directorio: " + directorioFTP);
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("Proceso Finalizado en Todas las Carpetas FTP.");
                logProcesos.registrarLog("OperadorDirectorios", "Proceso Finalizado en Todas las Carpetas FTP.", 1);
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                logProcesos.registrarLog("OperadorDirectorios", "Error Generando Listado de Archivos por Directorio: " +  ex, 0);
                Console.WriteLine("Error Generando Listado de Archivos por Directorio: " + ex);
            }
            finally
            {
                lectorStramsResultados.Close();
                streamResultados.Close();
            }
        }
    }

}
