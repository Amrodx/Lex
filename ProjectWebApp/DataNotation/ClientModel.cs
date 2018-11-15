using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWebApp.DataNotation
{
    public class ClientModel
    {
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public long ID_CLIENTE { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [RutValidation(ErrorMessage = "Ingrese un Rut Valido.")]
        public string RUT { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string NOMBRE_RAZON_SOCIAL { get; set; }
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public long ID_TIPO { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string DIRECCION { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [EmailAddress(ErrorMessage = "E-mail no es correcto.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "No permite los caracteres especiales.")]
        public string CORREO { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string CONTACTO { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "No es un numero de celular valido.")]
        public string FONO1 { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string FONO2 { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public long ID_COMUNA { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string OBSERVACIONES { get; set; }
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public string TIMESTAMP { get; set; }
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public string STATUS_ACTIVACION { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public Nullable<long> ID_PLAN { get; set; }
        //[Required(ErrorMessage = "El campo es obligatorio.")]
        public Nullable<long> ID_USUARIO { get; set; }
    }
}