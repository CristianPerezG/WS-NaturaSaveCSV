using System;
using System.IO;

namespace ServicioNaturaSaveCSV.Logs
{
    class LogSaveNatura
    {
        private StreamWriter objetoStream;
        private readonly string rutaRaizLogs = Properties.Settings.Default.RutaLogsNatura;
        private readonly string nombreArchivoErrores = Properties.Settings.Default.NombreArchivoError;
        private readonly string nombreArchivoBulk = Properties.Settings.Default.NombreArchivoBulk;
        private readonly string nombreArchivoHilo = Properties.Settings.Default.NombreArchivoHilos;

        public void registrarLog(string claseProceso, string mensajeProceso, int procesoSolicitado)
        {
            /*
             Case para escribir en el LOG 
                - 0: Si se dese escribir en el LOG de Errores
                - 1: Si se desea escribir en el LOG de Hilos
                - 2: Si desea escribir en el LOG de BULKS SQL             
             */
            switch (procesoSolicitado)
            {
                case 0:
                    objetoStream = File.Exists(rutaRaizLogs + nombreArchivoErrores)
                                 ? File.AppendText(rutaRaizLogs + nombreArchivoErrores)
                                 : new StreamWriter(rutaRaizLogs + nombreArchivoErrores);

                    objetoStream.WriteLine("/*/*/*/*/*/*/*/*/*");
                    objetoStream.WriteLine("Fecha del Proceso: " + DateTime.Now);
                    objetoStream.WriteLine("Clase:" + claseProceso);
                    objetoStream.WriteLine("Error: " + mensajeProceso);
                    objetoStream.WriteLine("/*/*/*/*/*/*/*/*/*");
                    objetoStream.WriteLine(" ");

                    break;
                case 1:
                    objetoStream = File.Exists(rutaRaizLogs + nombreArchivoHilo)
                                 ? File.AppendText(rutaRaizLogs + nombreArchivoHilo)
                                 : new StreamWriter(rutaRaizLogs + nombreArchivoHilo);

                    objetoStream.WriteLine("/*/*/*/*/*/*/*/*/*");
                    objetoStream.WriteLine("Fecha del Proceso: " + DateTime.Now);
                    objetoStream.WriteLine("Clase:" + claseProceso);
                    objetoStream.WriteLine("Mensaje: " + mensajeProceso);
                    objetoStream.WriteLine("/*/*/*/*/*/*/*/*/*");
                    objetoStream.WriteLine(" ");
                    break;
                case 2:
                    objetoStream = File.Exists(rutaRaizLogs + nombreArchivoBulk)
                                 ? File.AppendText(rutaRaizLogs + nombreArchivoBulk)
                                 : new StreamWriter(rutaRaizLogs + nombreArchivoBulk);

                    objetoStream.WriteLine("/*/*/*/*/*/*/*/*/*");
                    objetoStream.WriteLine("Fecha del Proceso: " + DateTime.Now);
                    objetoStream.WriteLine("Clase:" + claseProceso);
                    objetoStream.WriteLine("Mensaje: " + mensajeProceso);
                    objetoStream.WriteLine("/*/*/*/*/*/*/*/*/*");
                    objetoStream.WriteLine(" ");
                    break;
                default:
                    Console.WriteLine("LLEGA EN DEFAULT LA SOLICITUD DE REGISTRO EN EL LOG");
                    break;
            }

            objetoStream.Close();
        }
    }
}
