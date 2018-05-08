using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioNaturaSaveCSV
{
    static class ActividadPrincipal
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new ObtencionArchivosFTP()
            };

            ServiceBase.Run(ServicesToRun);

            //ObtencionArchivosFTP.generarHIlo();
            //while (true)
            //{

            //}

            //Hilos.HiloPrincipal hiloProncipal = new Hilos.HiloPrincipal();
            //hiloProncipal.generarHiloPrincipal();

        }
    }
}
