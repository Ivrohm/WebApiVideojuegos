namespace WebApiVideojuegos.Entidades
{
    public class Videojuego
    {
        public int Id { get; set; }

        public string name { get; set; }

        public List<EspecVideojuego> EspecVideojuegos { get; set; }
    }
}
