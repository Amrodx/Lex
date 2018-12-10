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
    
    public partial class CLIENTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTES()
        {
            this.PRESUPUESTO = new HashSet<PRESUPUESTO>();
        }
    
        public long ID_CLIENTE { get; set; }
        public string RUT { get; set; }
        public string NOMBRE_RAZON_SOCIAL { get; set; }
        public long ID_TIPO { get; set; }
        public string DIRECCION { get; set; }
        public string CORREO { get; set; }
        public string CONTACTO { get; set; }
        public string FONO1 { get; set; }
        public string FONO2 { get; set; }
        public long ID_COMUNA { get; set; }
        public string OBSERVACIONES { get; set; }
        public string TIMESTAMP { get; set; }
        public string STATUS_ACTIVACION { get; set; }
        public Nullable<long> ID_PLAN { get; set; }
        public Nullable<long> ID_USUARIO { get; set; }
    
        public virtual COMUNAS COMUNAS { get; set; }
        public virtual TIPO_CLIENTE TIPO_CLIENTE { get; set; }
        public virtual PLAN_PAGO PLAN_PAGO { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESUPUESTO> PRESUPUESTO { get; set; }
    }
}
