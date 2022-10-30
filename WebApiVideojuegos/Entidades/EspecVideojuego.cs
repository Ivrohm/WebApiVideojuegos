using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using WebApiVideojuegos.Validaciones;
//Data annotantions proporciona claases de atributos
namespace WebApiVideojuegos.Entidades
{
    public class EspecVideojuego
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "El campo name {0} es requerido")]//para llenar el campo, predefinidas por .net
        [PrimerLetraMayus]
        public string name { get; set; }

        //[Range(typeof(DateTime),"01/01/1997","01/01/2022",ErrorMessage = "La fecha de lanzamiento no esta dentro del rango ")]
        [Range(1990, 2022, ErrorMessage = "La fecha de lanzamiento no esta dentro del rango ")]
        [NotMapped]
        public string fechaLanzamiento { get; set; }

        public int VideojuegoId { get; set; }
        [Required(ErrorMessage = "El campo consola {0} es requerido")]
        [PrimerLetraMayus]
        public string consola { get; set; }


        // se pueden crear las validaciones y personalizarlas 
       
        }
}
