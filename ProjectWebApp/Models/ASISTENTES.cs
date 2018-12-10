//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ASISTENTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASISTENTES()
        {
            this.CONTRATOS = new HashSet<CONTRATOS>();
            this.PRESUPUESTO = new HashSet<PRESUPUESTO>();
        }
    
        public long ID_ASISTENTE { get; set; }
        public string RUT { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string CARGO { get; set; }
        public string TITULO_ACADEMICO { get; set; }
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
        public Nullable<long> ID_USUARIO { get; set; }
        public string CORREO { get; set; }
        public string DIRECCION { get; set; }
        public string FONO { get; set; }
        public Nullable<long> ID_COMUNA { get; set; }
    
        public virtual USUARIOS USUARIOS { get; set; }
        public virtual COMUNAS COMUNAS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATOS> CONTRATOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESUPUESTO> PRESUPUESTO { get; set; }
    }
}
