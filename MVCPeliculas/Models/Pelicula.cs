using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPeliculas.Models
{
    /// <summary>
    /// DEPRECATED: Use MVCPeliculas.Core.Entities.Pelicula instead.
    /// This class is kept for backward compatibility only.
    /// The DbContext now uses Core.Entities.Pelicula as the EF Core model.
    /// </summary>
    [Obsolete("Use MVCPeliculas.Core.Entities.Pelicula instead", false)]
    public class Pelicula
    {
        public int Id { get; set; }
        [StringLength(250, MinimumLength =3, ErrorMessage ="Debe escribir al menos 3 letras")]

        [Required]
        [Display(Name ="Título")]
        public string Titulo { get; set; }

        [Display(Name = "Fecha de lanzamiento")]
        [DataType(DataType.Date)]

        public DateTime FechaLanzamiento { get; set; }

     

        [Column(TypeName = "money")]
        [Required]
        [Range(1,100,ErrorMessage ="El precio debe estar entre 1 y 100")]
        public decimal Precio { get; set; }

        [StringLength(15)]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$",
            ErrorMessage = "El nombre del director debe comenzar con mayuscula y solo puede contener lestras, espacios, comillas y guiones")]
        public string Director { get; set; } // <- Nuevo

        [Required]
        public int GeneroId { get; set; } // <- LLAVE FORANEA

        [Display(Name ="Género")]
        public Genero? Genero { get; set; } // PROPIEDAD DE NAVEGACION


    }
}


