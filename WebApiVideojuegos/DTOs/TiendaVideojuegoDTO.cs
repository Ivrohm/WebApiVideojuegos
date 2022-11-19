using WebApiVideojuegos.Entidades;

namespace WebApiVideojuegos.DTOs
{
    public class TiendaVideojuegoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ReseñaVideojuegoDTO> Reseñas { get; set; }

    }
}
