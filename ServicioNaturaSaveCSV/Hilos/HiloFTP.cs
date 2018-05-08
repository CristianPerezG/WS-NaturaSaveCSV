using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System;
using ServicioNaturaSaveCSV.Logs;

using System.Linq;
using ServicioNaturaSaveCSV.Operadores;

namespace ServicioNaturaSaveCSV.Hilos
{
    class HiloFTP
    {

        private Thread nuevoHilo;
        private List<string> listadoDirectoriosFTP = new List<string>();
        private List<string> listadoDirectoriosConW = new List<string>();
        private List<string> listadoDirectoriosSinW = new List<string>();
        private Timer timerFTP;
        private LogSaveNatura logProcesos = new LogSaveNatura();
        private OperadorDirectorios operadorDirectorios = new OperadorDirectorios();
        private readonly int tiempoHiloFTP = Properties.Settings.Default.TiempoHiloFTP;
        private OperadorWatcher operadorWatcher = new OperadorWatcher();
        private bool confirmaWatchers = true;
        private readonly OperadorSQL operadorSQL = new OperadorSQL();


        public void generarHiloFTP()
        {
            nuevoHilo = new Thread(new ThreadStart(Run));
            nuevoHilo.Start();
        }

        private void Run()
        {
            timerFTP = new Timer(procesoPrincipalFPT, null, 0, tiempoHiloFTP);
        }

        private void procesoPrincipalFPT(Object o)
        {
            listadoDirectoriosFTP = operadorDirectorios.verificarEstructuraDirectorios();            
            listadoDirectoriosFTP.AddRange(operadorSQL.obtenerDirectoriosParam());

            if (confirmaWatchers)
            {
                operadorWatcher.generarWatchersLocales(listadoDirectoriosFTP);
                listadoDirectoriosConW = listadoDirectoriosFTP;
                confirmaWatchers = false;
            }
            else
            {
                listadoDirectoriosFTP = operadorDirectorios.verificarEstructuraDirectorios();
                listadoDirectoriosFTP.AddRange(operadorSQL.obtenerDirectoriosParam());                

                if (listadoDirectoriosFTP.Count > listadoDirectoriosConW.Count)
                {
                    IEnumerable<string> listadoDirectoriosSinW = listadoDirectoriosFTP.Except(listadoDirectoriosConW);
                    foreach (string directorioFaltante in listadoDirectoriosSinW)
                    {
                        operadorWatcher.generarWatcherDirectorio(directorioFaltante);
                    }
                }
            }

            operadorDirectorios.listarArchivosDirectorio(listadoDirectoriosFTP);
        }
    }
}
