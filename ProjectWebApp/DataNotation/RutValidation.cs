using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebApp.DataNotation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RutValidation : ValidationAttribute, IClientValidatable
    {
        public string otherPropertyName { get; set; }

        public RutValidation()
        {
            otherPropertyName = "hola";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return validarRut(value as string) ? null : new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        public bool validarRut(string rut)
        {
            if (rut == null || rut.Length == 0)
            {
                return true;
            }
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //string errorMessage = this.FormatErrorMessage(metadata.DisplayName);
            string errorMessage = ErrorMessageString;

            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule rutvalidation = new ModelClientValidationRule();
            rutvalidation.ErrorMessage = errorMessage;
            rutvalidation.ValidationType = "rutvalidation"; // This is the name the jQuery adapter will use
            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            //rutvalidation.ValidationParameters.Add("otherpropertyname", otherPropertyName);

            yield return rutvalidation;
        }
    }
}