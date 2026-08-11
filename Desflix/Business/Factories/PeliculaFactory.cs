using Desflix.Core.Entities;
using Desflix.DTOs.Request;

namespace Desflix.Business.Factories
{
    /// <summary>
    /// Factory para crear objetos Pelicula
    /// Patrón: Factory Pattern
    /// Centraliza la lógica de creación de objetos Pelicula
    /// Facilita cambios futuros en la construcción de objetos
    /// </summary>
    public interface IPeliculaFactory
    {
        Pelicula CrearDesdeDTO(CrearPeliculaRequestDto dto);
        Pelicula CrearPeliculaDefault();
    }

    public class PeliculaFactory : IPeliculaFactory
    {
        private const decimal PrecioMinimo = 0.99m;
        private const string UrlImagenDefault = "https://via.placeholder.com/300x450?text=Sin+Imagen";

        public Pelicula CrearDesdeDTO(CrearPeliculaRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Pelicula
            {
                Titulo = dto.Titulo.Trim(),
                Sinopsis = dto.Sinopsis?.Trim(),
                FechaLanzamiento = dto.FechaLanzamiento,
                Precio = Math.Max(dto.Precio, PrecioMinimo),
                Director = dto.Director.Trim(),
                UrlImagen = string.IsNullOrEmpty(dto.UrlImagen) ? UrlImagenDefault : dto.UrlImagen,
                Calificacion = ValidarCalificacion(dto.Calificacion),
                GeneroId = dto.GeneroId,
                FechaCreacion = DateTime.UtcNow,
                FechaModificacion = null,
                Activo = true
            };
        }

        public Pelicula CrearPeliculaDefault()
        {
            return new Pelicula
            {
                Titulo = "Nueva Película",
                Sinopsis = string.Empty,
                FechaLanzamiento = DateTime.Now,
                Precio = PrecioMinimo,
                Director = "Sin asignar",
                UrlImagen = UrlImagenDefault,
                Calificacion = 0,
                GeneroId = 1, // Suponiendo que existe el género con id 1
                FechaCreacion = DateTime.UtcNow,
                Activo = true
            };
        }

        private decimal? ValidarCalificacion(decimal? calificacion)
        {
            if (!calificacion.HasValue)
                return null;

            return Math.Clamp(calificacion.Value, 0, 10);
        }
    }
}
