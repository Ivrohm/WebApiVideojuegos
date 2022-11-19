using WebApiVideojuegos.Entidades;
using WebApiVideojuegos.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace WebApiVideojuegos.DTOs
{
    public class VideojuegoDTO
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]

        public string Titulo { get; set; }
        public string añodeLanzamiento { get; set; }

        public string consola { get; set; }

       
    }
}
