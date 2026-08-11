using System.ComponentModel.DataAnnotations;

namespace Desflix.Core.Entities
{
    /// <summary>
    /// Entidad de dominio Genero
    /// Representa un género de película
    /// Patrón: Entity
    /// </summary>
    public class Genero
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El nombre del género es obligatorio")]
        [Display(Name = "Género")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [StringLength(500)]
        public string? Descripcion { get; set; }

        // Relación inversa con Películas
        public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();

        // Metadatos
        [Display(Name = "Creado")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}
