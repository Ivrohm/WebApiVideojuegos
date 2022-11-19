using System.ComponentModel.DataAnnotations;
using WebApiVideojuegos.Validaciones;

namespace WebApiVideojuegos.Entidades
{
    public class TiendaVideojuego
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimerLetraMayus]
        
        public string Name { get; set; }
      
        public List<Reseña> Reseñas { get; set; }
        public List<VideojuegoTiendaVideojuego> VideojuegoTiendaVideojuego { get; set; }
    }
}
