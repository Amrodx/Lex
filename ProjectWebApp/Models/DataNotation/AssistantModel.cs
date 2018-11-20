using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWebApp.DataNotation
{
    public class AssistantModel
    {
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public long ID_ASISTENTE { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string RUT { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string NOMBRES { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string APELLIDO_PATERNO { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string APELLIDO_MATERNO { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string CARGO { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string TITULO_ACADEMICO { get; set; }
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public Nullable<long> ID_USUARIO { get; set; }
    }
}