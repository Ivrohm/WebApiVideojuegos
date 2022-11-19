using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApiVideojuegos.Validaciones;

namespace WebApiVideojuegos.Entidades
{
    public class VideojuegoTiendaVideojuego
    {
        public int VideojuegoId { get; set; }
        public int tiendaVideojuegoId { get; set; }
        public int orden { get; set; }
        public TiendaVideojuego tiendaVideojuego { get; set; }
        public Videojuego Videojuego { get; set; }
    }
}
