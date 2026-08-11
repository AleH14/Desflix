using MVCPeliculas.Core.Entities;
using MVCPeliculas.Core.Interfaces;
using MVCPeliculas.DTOs.Request;
using MVCPeliculas.DTOs.Response;

namespace MVCPeliculas.Business.Services
{
    /// <summary>
    /// Servicio de Géneros
    /// Patrón: Service Layer
    /// Encapsula la lógica de negocio de géneros
    /// </summary>
    public class GeneroService : IGeneroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GeneroService> _logger;

        public GeneroService(IUnitOfWork unitOfWork, ILogger<GeneroService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GeneroResponseDto?> GetGeneroByIdAsync(int id)
        {
            try
            {
                var genero = await _unitOfWork.Generos.GetWithMoviesAsync(id);
                return genero == null ? null : MapToResponseDto(genero);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting género with id {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<GeneroResponseDto>> GetAllGenerosAsync()
        {
            try
            {
                var generos = await _unitOfWork.Generos.GetAllAsync();
                return generos
                    .Where(g => g.Activo)
                    .Select(MapToResponseDto)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting all géneros: {ex.Message}");
                throw;
            }
        }

        public async Task<GeneroResponseDto> CreateGeneroAsync(CrearGeneroRequestDto dto)
        {
            try
            {
                // Validar que no exista un género con el mismo nombre
                var existente = await _unitOfWork.Generos.GetByNombreAsync(dto.Nombre);
                if (existente != null)
                    throw new InvalidOperationException($"Ya existe un género con el nombre '{dto.Nombre}'");

                var genero = new Genero
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                };

                await _unitOfWork.Generos.AddAsync(genero);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Género '{genero.Nombre}' creado exitosamente");
                return MapToResponseDto(genero);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating género: {ex.Message}");
                throw;
            }
        }

        public async Task<GeneroResponseDto> UpdateGeneroAsync(int id, CrearGeneroRequestDto dto)
        {
            try
            {
                var genero = await _unitOfWork.Generos.GetByIdAsync(id);
                if (genero == null)
                    throw new InvalidOperationException($"Género con id {id} no encontrado");

                // Validar que no exista otro género con el mismo nombre
                if (!genero.Nombre.Equals(dto.Nombre, StringComparison.OrdinalIgnoreCase))
                {
                    var existente = await _unitOfWork.Generos.GetByNombreAsync(dto.Nombre);
                    if (existente != null)
                        throw new InvalidOperationException($"Ya existe un género con el nombre '{dto.Nombre}'");
                }

                genero.Nombre = dto.Nombre;
                genero.Descripcion = dto.Descripcion;

                _unitOfWork.Generos.Update(genero);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Género '{genero.Nombre}' actualizado exitosamente");
                return MapToResponseDto(genero);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating género: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteGeneroAsync(int id)
        {
            try
            {
                var genero = await _unitOfWork.Generos.GetByIdAsync(id);
                if (genero == null)
                    throw new InvalidOperationException($"Género con id {id} no encontrado");

                // Validar que no tenga películas activas
                var generoConPeliculas = await _unitOfWork.Generos.GetWithMoviesAsync(id);
                if (generoConPeliculas?.Peliculas.Any(p => p.Activo) == true)
                    throw new InvalidOperationException($"No se puede eliminar el género '{genero.Nombre}' porque tiene películas asociadas");

                // Soft delete
                genero.Activo = false;
                _unitOfWork.Generos.Update(genero);
                var result = await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Género con id {id} eliminado (soft delete)");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting género: {ex.Message}");
                throw;
            }
        }

        // Mapeo interno
        private GeneroResponseDto MapToResponseDto(Genero genero)
        {
            return new GeneroResponseDto
            {
                Id = genero.Id,
                Nombre = genero.Nombre,
                Descripcion = genero.Descripcion,
                CantidadPeliculas = genero.Peliculas.Count(p => p.Activo),
                FechaCreacion = genero.FechaCreacion,
                Activo = genero.Activo
            };
        }
    }
}
