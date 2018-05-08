using System;
using System.ServiceProcess;
using System.Threading;
using ServicioNaturaSaveCSV.Hilos;
using System.Runtime.InteropServices;
using ServicioNaturaSaveCSV.Logs;

namespace ServicioNaturaSaveCSV
{
    public partial class ObtencionArchivosFTP : ServiceBase
    {        
        private static HiloFTP hiloFTP = new HiloFTP();
        private LogSaveNatura logProcesos = new LogSaveNatura();
        private readonly int tiempoHiloFTP = Properties.Settings.Default.TiempoHiloFTP;

        //[DllImport("kernel32.dll")]
        //static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //const int SW_HIDE = 0;
        //const int SW_SHOW = 5;

        public ObtencionArchivosFTP()
        {            
        }

        protected override void OnStart(string[] args)
        {
            hiloFTP.generarHiloFTP();            
        }

        protected override void OnStop()
        {
            logProcesos.registrarLog("Servicio Windows", "Incia Servicio WINDOWS de manera Correcta - Se inicia el primer proceso FTP.", 1);
        }

        public static void generarHIlo() {
            hiloFTP.generarHiloFTP();
        }
    }
}
