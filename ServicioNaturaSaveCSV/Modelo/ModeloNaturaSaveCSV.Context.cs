﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicioNaturaSaveCSV.Modelo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InfoNaturaEntities : DbContext
    {
        public InfoNaturaEntities()
            : base("name=InfoNaturaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EstadoAuditoriaSaveCSV> EstadoAuditoriaSaveCSV { get; set; }
        public virtual DbSet<AuditoriaNaturaSaveCSV> AuditoriaNaturaSaveCSV { get; set; }
    
        public virtual ObjectResult<SP_ETL_ActualizarRegistroAuditoria_Result> SP_ETL_ActualizarRegistroAuditoria(Nullable<int> idProceso, string nombreArchivo, string rutaArchivo, string descripcionProceso, Nullable<int> estadoProceso, Nullable<bool> estadoDescarga, Nullable<bool> estadoCodificacion, Nullable<bool> estadoBulk)
        {
            var idProcesoParameter = idProceso.HasValue ?
                new ObjectParameter("idProceso", idProceso) :
                new ObjectParameter("idProceso", typeof(int));
    
            var nombreArchivoParameter = nombreArchivo != null ?
                new ObjectParameter("nombreArchivo", nombreArchivo) :
                new ObjectParameter("nombreArchivo", typeof(string));
    
            var rutaArchivoParameter = rutaArchivo != null ?
                new ObjectParameter("rutaArchivo", rutaArchivo) :
                new ObjectParameter("rutaArchivo", typeof(string));
    
            var descripcionProcesoParameter = descripcionProceso != null ?
                new ObjectParameter("descripcionProceso", descripcionProceso) :
                new ObjectParameter("descripcionProceso", typeof(string));
    
            var estadoProcesoParameter = estadoProceso.HasValue ?
                new ObjectParameter("estadoProceso", estadoProceso) :
                new ObjectParameter("estadoProceso", typeof(int));
    
            var estadoDescargaParameter = estadoDescarga.HasValue ?
                new ObjectParameter("estadoDescarga", estadoDescarga) :
                new ObjectParameter("estadoDescarga", typeof(bool));
    
            var estadoCodificacionParameter = estadoCodificacion.HasValue ?
                new ObjectParameter("estadoCodificacion", estadoCodificacion) :
                new ObjectParameter("estadoCodificacion", typeof(bool));
    
            var estadoBulkParameter = estadoBulk.HasValue ?
                new ObjectParameter("estadoBulk", estadoBulk) :
                new ObjectParameter("estadoBulk", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ETL_ActualizarRegistroAuditoria_Result>("SP_ETL_ActualizarRegistroAuditoria", idProcesoParameter, nombreArchivoParameter, rutaArchivoParameter, descripcionProcesoParameter, estadoProcesoParameter, estadoDescargaParameter, estadoCodificacionParameter, estadoBulkParameter);
        }
    
        public virtual ObjectResult<SP_ETL_CrearRegistroAuditoria_Result> SP_ETL_CrearRegistroAuditoria(string nombreArchivo, string rutaArchivo, string descripcionProceso, Nullable<int> estadoProceso, Nullable<bool> estadoDescarga, Nullable<bool> estadoCodificacion, Nullable<bool> estadoBulk)
        {
            var nombreArchivoParameter = nombreArchivo != null ?
                new ObjectParameter("nombreArchivo", nombreArchivo) :
                new ObjectParameter("nombreArchivo", typeof(string));
    
            var rutaArchivoParameter = rutaArchivo != null ?
                new ObjectParameter("rutaArchivo", rutaArchivo) :
                new ObjectParameter("rutaArchivo", typeof(string));
    
            var descripcionProcesoParameter = descripcionProceso != null ?
                new ObjectParameter("descripcionProceso", descripcionProceso) :
                new ObjectParameter("descripcionProceso", typeof(string));
    
            var estadoProcesoParameter = estadoProceso.HasValue ?
                new ObjectParameter("estadoProceso", estadoProceso) :
                new ObjectParameter("estadoProceso", typeof(int));
    
            var estadoDescargaParameter = estadoDescarga.HasValue ?
                new ObjectParameter("estadoDescarga", estadoDescarga) :
                new ObjectParameter("estadoDescarga", typeof(bool));
    
            var estadoCodificacionParameter = estadoCodificacion.HasValue ?
                new ObjectParameter("estadoCodificacion", estadoCodificacion) :
                new ObjectParameter("estadoCodificacion", typeof(bool));
    
            var estadoBulkParameter = estadoBulk.HasValue ?
                new ObjectParameter("estadoBulk", estadoBulk) :
                new ObjectParameter("estadoBulk", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ETL_CrearRegistroAuditoria_Result>("SP_ETL_CrearRegistroAuditoria", nombreArchivoParameter, rutaArchivoParameter, descripcionProcesoParameter, estadoProcesoParameter, estadoDescargaParameter, estadoCodificacionParameter, estadoBulkParameter);
        }
    
        public virtual ObjectResult<SP_ETL_Obtener_Directorios_Result> SP_ETL_Obtener_Directorios()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ETL_Obtener_Directorios_Result>("SP_ETL_Obtener_Directorios");
        }
    }
}
