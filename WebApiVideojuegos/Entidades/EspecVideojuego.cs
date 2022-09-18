namespace WebApiVideojuegos.Entidades
{
    public class EspecVideojuego
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string fechaLanzamiento { get; set; }

        public int JuegoId { get; set; }

        public string consola { get; set; }

        public Videojuego Videojuego { get; set; }

    }
}
