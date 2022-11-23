using System.ComponentModel.DataAnnotations;

namespace WebApiVideojuegos.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
