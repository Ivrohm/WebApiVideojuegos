namespace WebApiVideojuegos.DTOs
{
    public class VideojuegoDTOConTiendaVideojuego : GetVideojuegoDTO
    {
        public List<TiendaVideojuegoDTO> TiendaVideojuego { get; set; }
    }
}
