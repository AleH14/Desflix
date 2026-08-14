using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desflix.Core.Entities
{
    /// <summary>
    /// Entidad de dominio Pelicula
    /// Representa una película en el catálogo de Desflix
    /// Patrón: Entity
    /// </summary>
    public class Pelicula
    {
        public int Id { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = "Debe escribir al menos 3 letras")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Sinopsis")]
        [StringLength(1000)]
        public string? Sinopsis { get; set; }

        [Display(Name = "Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.99, 100, ErrorMessage = "El precio debe estar entre 0.99 y 100")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El director es obligatorio")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s'-]+$", 
            ErrorMessage = "El nombre del director debe comenzar con mayúscula y solo puede contener letras, espacios, comillas y guiones")]
        [Display(Name = "Director")]
        public string Director { get; set; } = string.Empty;

        [Display(Name = "URL Imagen")]
        [Url(ErrorMessage = "Debe ser una URL válida")]
        [StringLength(500)]
        public string? UrlImagen { get; set; }

        // Propiedad para calificación de películas (0-10)
        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10")]
        [Display(Name = "Calificación")]
        public decimal? Calificacion { get; set; }

        // Relación con Género (Foreign Key)
        [Required(ErrorMessage = "Debe seleccionar un género")]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }

        // Propiedad de navegación
        public virtual Genero? Genero { get; set; }

        // Metadatos
        [Display(Name = "Creado")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Display(Name = "Modificado")]
        public DateTime? FechaModificacion { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}
