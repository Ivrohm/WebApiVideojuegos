
using System.ComponentModel.DataAnnotations;


namespace WebApiVideojuegos.Entidades
{
    public class Videojuego
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo name {0} es requerido")]
   
        public string name { get; set; }

        public List<EspecVideojuego> EspecVideojuegos { get; set; }
    }
}
