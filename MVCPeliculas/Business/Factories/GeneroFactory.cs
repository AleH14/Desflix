using MVCPeliculas.Core.Entities;
using MVCPeliculas.DTOs.Request;

namespace MVCPeliculas.Business.Factories
{
    /// <summary>
    /// Factory para crear objetos Genero
    /// Patrón: Factory Pattern
    /// Centraliza la lógica de creación de objetos Genero
    /// </summary>
    public interface IGeneroFactory
    {
        Genero CrearDesdeDTO(CrearGeneroRequestDto dto);
        Genero CrearGeneroDefault();
    }

    public class GeneroFactory : IGeneroFactory
    {
        public Genero CrearDesdeDTO(CrearGeneroRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Genero
            {
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion?.Trim(),
                FechaCreacion = DateTime.UtcNow,
                Activo = true,
                Peliculas = new List<Pelicula>()
            };
        }

        public Genero CrearGeneroDefault()
        {
            return new Genero
            {
                Nombre = "Sin Clasificar",
                Descripcion = "Películas sin género específico",
                FechaCreacion = DateTime.UtcNow,
                Activo = true,
                Peliculas = new List<Pelicula>()
            };
        }
    }
}
