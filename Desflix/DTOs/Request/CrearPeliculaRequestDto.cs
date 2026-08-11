using System.ComponentModel.DataAnnotations;

namespace Desflix.DTOs.Request
{
    /// <summary>
    /// DTO para crear una nueva película
    /// Patrón: Data Transfer Object (DTO)
    /// Separa la estructura de entrada de la entidad de dominio
    /// </summary>
    public class CrearPeliculaRequestDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "El título debe tener entre 3 y 250 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La sinopsis no puede exceder 1000 caracteres")]
        [Display(Name = "Sinopsis")]
        public string? Sinopsis { get; set; }

        [Required(ErrorMessage = "La fecha de lanzamiento es obligatoria")]
        [Display(Name = "Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.99, 100, ErrorMessage = "El precio debe estar entre 0.99 y 100")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El director es obligatorio")]
        [StringLength(100, ErrorMessage = "El directormente no puede exceder 100 caracteres")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s'-]+$",
            ErrorMessage = "El nombre del director debe comenzar con mayúscula")]
        [Display(Name = "Director")]
        public string Director { get; set; } = string.Empty;

        [Display(Name = "URL Imagen")]
        [Url(ErrorMessage = "Debe ser una URL válida")]
        [StringLength(500)]
        public string? UrlImagen { get; set; }

        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10")]
        [Display(Name = "Calificación")]
        public decimal? Calificacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un género")]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }
    }
}
