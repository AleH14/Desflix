using Desflix.Core.Entities;
using Desflix.Core.Interfaces;
using Desflix.DTOs.Request;
using Desflix.DTOs.Response;

namespace Desflix.Business.Services
{
    /// <summary>
    /// Servicio de Películas
    /// Patrón: Service Layer
    /// Encapsula la lógica de negocio de películas
    /// Coordina entre Controllers y Repositories
    /// </summary>
    public class PeliculaService : IPeliculaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PeliculaService> _logger;

        public PeliculaService(IUnitOfWork unitOfWork, ILogger<PeliculaService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PeliculaResponseDto?> GetPeliculaByIdAsync(int id)
        {
            try
            {
                var pelicula = await _unitOfWork.Peliculas.GetWithGeneroAsync(id);
                return pelicula == null ? null : MapToResponseDto(pelicula);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting película with id {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<PeliculaResponseDto>> GetAllPeliculasAsync()
        {
            try
            {
                var peliculas = await _unitOfWork.Peliculas.GetAllAsync();
                return peliculas
                    .Where(p => p.Activo)
                    .OrderBy(p => p.Titulo)
                    .Select(MapToResponseDto)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting all películas: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<PeliculaResponseDto>> SearchPeliculasAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllPeliculasAsync();

            try
            {
                var peliculas = await _unitOfWork.Peliculas.SearchAsync(searchTerm);
                return peliculas.Select(MapToResponseDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error searching películas: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<PeliculaResponseDto>> GetPeliculasByGeneroAsync(int generoId)
        {
            try
            {
                var peliculas = await _unitOfWork.Peliculas.GetByGeneroAsync(generoId);
                return peliculas.Select(MapToResponseDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting películas by género {generoId}: {ex.Message}");
                throw;
            }
        }

        public async Task<PeliculaResponseDto> CreatePeliculaAsync(CrearPeliculaRequestDto dto)
        {
            try
            {
                // Validar que el género exista
                var genero = await _unitOfWork.Generos.GetByIdAsync(dto.GeneroId);
                if (genero == null)
                    throw new InvalidOperationException($"El género con id {dto.GeneroId} no existe");

                var pelicula = new Pelicula
                {
                    Titulo = dto.Titulo,
                    Sinopsis = dto.Sinopsis,
                    FechaLanzamiento = dto.FechaLanzamiento,
                    Precio = dto.Precio,
                    Director = dto.Director,
                    UrlImagen = dto.UrlImagen,
                    Calificacion = dto.Calificacion,
                    GeneroId = dto.GeneroId,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                };

                await _unitOfWork.Peliculas.AddAsync(pelicula);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Película '{pelicula.Titulo}' creada exitosamente");
                return MapToResponseDto(pelicula);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating película: {ex.Message}");
                throw;
            }
        }

        public async Task<PeliculaResponseDto> UpdatePeliculaAsync(int id, ActualizarPeliculaRequestDto dto)
        {
            try
            {
                var pelicula = await _unitOfWork.Peliculas.GetByIdAsync(id);
                if (pelicula == null)
                    throw new InvalidOperationException($"Película con id {id} no encontrada");

                // Validar que el género exista
                var genero = await _unitOfWork.Generos.GetByIdAsync(dto.GeneroId);
                if (genero == null)
                    throw new InvalidOperationException($"El género con id {dto.GeneroId} no existe");

                pelicula.Titulo = dto.Titulo;
                pelicula.Sinopsis = dto.Sinopsis;
                pelicula.FechaLanzamiento = dto.FechaLanzamiento;
                pelicula.Precio = dto.Precio;
                pelicula.Director = dto.Director;
                pelicula.UrlImagen = dto.UrlImagen;
                pelicula.Calificacion = dto.Calificacion;
                pelicula.GeneroId = dto.GeneroId;
                pelicula.FechaModificacion = DateTime.UtcNow;

                _unitOfWork.Peliculas.Update(pelicula);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Película '{pelicula.Titulo}' actualizada exitosamente");
                return MapToResponseDto(pelicula);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating película: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeletePeliculaAsync(int id)
        {
            try
            {
                var pelicula = await _unitOfWork.Peliculas.GetByIdAsync(id);
                if (pelicula == null)
                    throw new InvalidOperationException($"Película con id {id} no encontrada");

                // Soft delete
                pelicula.Activo = false;
                pelicula.FechaModificacion = DateTime.UtcNow;

                _unitOfWork.Peliculas.Update(pelicula);
                var result = await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Película con id {id} eliminada (soft delete)");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting película: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<PeliculaResponseDto>> GetTopRatedAsync(int count = 10)
        {
            try
            {
                var peliculas = await _unitOfWork.Peliculas.GetTopRatedAsync(count);
                return peliculas.Select(MapToResponseDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting top rated películas: {ex.Message}");
                throw;
            }
        }

        // Mapeos internos
        private PeliculaResponseDto MapToResponseDto(Pelicula pelicula)
        {
            return new PeliculaResponseDto
            {
                Id = pelicula.Id,
                Titulo = pelicula.Titulo,
                Sinopsis = pelicula.Sinopsis,
                FechaLanzamiento = pelicula.FechaLanzamiento,
                Precio = pelicula.Precio,
                Director = pelicula.Director,
                UrlImagen = pelicula.UrlImagen,
                Calificacion = pelicula.Calificacion,
                Genero = pelicula.Genero != null ? new GeneroResponseDto
                {
                    Id = pelicula.Genero.Id,
                    Nombre = pelicula.Genero.Nombre,
                    Descripcion = pelicula.Genero.Descripcion,
                    Activo = pelicula.Genero.Activo,
                    CantidadPeliculas = pelicula.Genero.Peliculas.Count(p => p.Activo)
                } : null,
                FechaCreacion = pelicula.FechaCreacion,
                FechaModificacion = pelicula.FechaModificacion,
                Activo = pelicula.Activo
            };
        }
    }
}
