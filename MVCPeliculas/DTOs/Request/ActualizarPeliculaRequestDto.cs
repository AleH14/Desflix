using System.ComponentModel.DataAnnotations;

namespace MVCPeliculas.DTOs.Request
{
    /// <summary>
    /// DTO para actualizar una película existente
    /// Patrón: Data Transfer Object (DTO)
    /// </summary>
    public class ActualizarPeliculaRequestDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(250, MinimumLength = 3)]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Sinopsis")]
        public string? Sinopsis { get; set; }

        [Required(ErrorMessage = "La fecha de lanzamiento es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.99, 100)]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El director es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Director")]
        public string Director { get; set; } = string.Empty;

        [Url]
        [StringLength(500)]
        [Display(Name = "URL Imagen")]
        public string? UrlImagen { get; set; }

        [Range(0, 10)]
        [Display(Name = "Calificación")]
        public decimal? Calificacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un género")]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }
    }
}
