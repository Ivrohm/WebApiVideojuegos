
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using WebApiVideojuegos.Validaciones;

namespace WebApiVideojuegos.Entidades
{
    public class Videojuego : IValidatableObject
    {
        public int Id { get; set; }

        //[NotMapped]
        [Required(ErrorMessage = "El campo name {0} es requerido")]//para llenar el campo, predefinidas por .net
        // [PrimerLetraMayus]
        public string name { get; set; }

        public List<EspecVideojuego> EspecVideojuegos { get; set; }

        [NotMapped]
        public int Menor { get; set; }
        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var primeraLetra = name[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] { nameof(name) });
                }
            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo mayor",
                    new String[] { nameof(Menor) });
            }
        }

    }
}
