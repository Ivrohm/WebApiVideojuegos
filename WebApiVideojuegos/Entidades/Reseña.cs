using Microsoft.AspNetCore.Identity;

namespace WebApiVideojuegos.Entidades
{
    public class Reseña
    {
        public int Id { get; set; }
        public string reseña { get; set; }
        public int tiendaVideojuegoId { get; set; }
       
        public TiendaVideojuego tiendaVideojuego { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }

    }
}
