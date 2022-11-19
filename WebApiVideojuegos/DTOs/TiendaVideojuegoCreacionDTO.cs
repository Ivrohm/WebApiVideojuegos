using WebApiVideojuegos.Validaciones;

using System.ComponentModel.DataAnnotations;

namespace WebApiVideojuegos.DTOs
{
    public class TiendaVideojuegoCreacionDTO
    {
     
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimerLetraMayus]

        public string Name { get; set; }
        
        public List<int> VideojuegosId { get; set; }
    }
}
