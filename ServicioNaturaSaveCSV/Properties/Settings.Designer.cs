﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicioNaturaSaveCSV.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\Sitios\\xm\\FTP_CSV\\")]
        public string RutaRaizNatura {
            get {
                return ((string)(this["RutaRaizNatura"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3600000")]
        public int TiempoHiloFTP {
            get {
                return ((int)(this["TiempoHiloFTP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("gera")]
        public string UsuarioFTP {
            get {
                return ((string)(this["UsuarioFTP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("y8Shy7$3")]
        public string PasswrodFTP {
            get {
                return ((string)(this["PasswrodFTP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ftp://209.172.62.232/")]
        public string HostFTP {
            get {
                return ((string)(this["HostFTP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".csv")]
        public string ExtensionArchivosFTP {
            get {
                return ((string)(this["ExtensionArchivosFTP"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\Sitios\\xm\\Logs\\FTPCSV\\")]
        public string RutaLogsNatura {
            get {
                return ((string)(this["RutaLogsNatura"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LogProcesoPrincipal.txt")]
        public string NombreArchivoHilos {
            get {
                return ((string)(this["NombreArchivoHilos"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LogError.txt")]
        public string NombreArchivoError {
            get {
                return ((string)(this["NombreArchivoError"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LogBulksSQL.txt")]
        public string NombreArchivoBulk {
            get {
                return ((string)(this["NombreArchivoBulk"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=10.21.1.16\\INFONATURA;Initial Catalog=InfoNatura;Persist Security Inf" +
            "o=True;User ID=info;Password=InfoNatura")]
        public string CadenaSQL {
            get {
                return ((string)(this["CadenaSQL"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".*(?<mes>(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\\s*(?<dia>[0-9]*)\\s*(?" +
            "<año>([0-9]|:)*)\\s*(?<nombreDirectorio>.*)")]
        public string ExpresionStringFTP {
            get {
                return ((string)(this["ExpresionStringFTP"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".tmp")]
        public string ExtensionArchivosLocal {
            get {
                return ((string)(this["ExtensionArchivosLocal"]));
            }
            set {
                this["ExtensionArchivosLocal"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SP_ETL_")]
        public string InicioSPNatura {
            get {
                return ((string)(this["InicioSPNatura"]));
            }
            set {
                this["InicioSPNatura"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Descargando")]
        public string EstadoDescargando {
            get {
                return ((string)(this["EstadoDescargando"]));
            }
            set {
                this["EstadoDescargando"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Cambiando Codificación")]
        public string EstadoCambioandoCod {
            get {
                return ((string)(this["EstadoCambioandoCod"]));
            }
            set {
                this["EstadoCambioandoCod"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("BulkInsert")]
        public string EstadoBulk {
            get {
                return ((string)(this["EstadoBulk"]));
            }
            set {
                this["EstadoBulk"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Finalizado")]
        public string EstadoFinalizado {
            get {
                return ((string)(this["EstadoFinalizado"]));
            }
            set {
                this["EstadoFinalizado"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Fallido")]
        public string EstadoFallido {
            get {
                return ((string)(this["EstadoFallido"]));
            }
            set {
                this["EstadoFallido"] = value;
            }
        }
    }
}