using System.ComponentModel.DataAnnotations;

namespace Desflix.DTOs.Request
{
    /// <summary>
    /// DTO para crear un nuevo género
    /// Patrón: Data Transfer Object (DTO)
    /// </summary>
    public class CrearGeneroRequestDto
    {
        [Required(ErrorMessage = "El nombre del género es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
    }
}
