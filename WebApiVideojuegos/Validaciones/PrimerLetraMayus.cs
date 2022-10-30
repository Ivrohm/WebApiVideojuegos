using System.ComponentModel.DataAnnotations;

namespace WebApiVideojuegos.Validaciones
{
    public class PrimerLetraMayus : ValidationAttribute // se implementa ValidationAttribute
    {
      
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)//se reciben los parametros de tipo value pueden ser string o booleanos
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))//se observa si existe o no existe con la validacion 
                {
                    return ValidationResult.Success;
                }

                var Primer_Letra = value.ToString()[0].ToString();// se obtiene el value de la primer letra desde 0 en adelante nuesto string, se convierte en string

                if (Primer_Letra != Primer_Letra.ToUpper()) // observa que la letra sea mayuscula, haciendo el uso del "ToUpper"
                {
                    return new ValidationResult("La primera letra a ingresar debe ser mayuscula"); //se define el mensaje personalizado que deseamos 
                }

                return ValidationResult.Success;// nos da el resultado de si es valido 
            }
        
    }
}
