using System.ComponentModel.DataAnnotations;
using WebApiVideojuegos.Validaciones;


namespace WebApiVideojuegos.Entidades
{
    public class Videojuego
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]
        [PrimerLetraMayus]
        public string Titulo { get; set; }
        public string AñodeLanzamiento { get; set; }

        public string consola { get; set; }

        public List<VideojuegoTiendaVideojuego> videojuegoTiendaVideojuegos{ get; set; }

      
    }
}
