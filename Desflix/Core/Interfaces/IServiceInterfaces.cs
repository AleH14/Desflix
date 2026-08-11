using Desflix.DTOs.Request;
using Desflix.DTOs.Response;

namespace Desflix.Core.Interfaces
{
    /// <summary>
    /// Interfaz para servicio de Películas
    /// Patrón: Service Layer
    /// Contiene la lógica de negocio de películas
    /// </summary>
    public interface IPeliculaService
    {
        Task<PeliculaResponseDto?> GetPeliculaByIdAsync(int id);
        Task<IEnumerable<PeliculaResponseDto>> GetAllPeliculasAsync();
        Task<IEnumerable<PeliculaResponseDto>> SearchPeliculasAsync(string searchTerm);
        Task<IEnumerable<PeliculaResponseDto>> GetPeliculasByGeneroAsync(int generoId);
        Task<PeliculaResponseDto> CreatePeliculaAsync(CrearPeliculaRequestDto dto);
        Task<PeliculaResponseDto> UpdatePeliculaAsync(int id, ActualizarPeliculaRequestDto dto);
        Task<bool> DeletePeliculaAsync(int id);
        Task<IEnumerable<PeliculaResponseDto>> GetTopRatedAsync(int count = 10);
    }

    /// <summary>
    /// Interfaz para servicio de Géneros
    /// </summary>
    public interface IGeneroService
    {
        Task<GeneroResponseDto?> GetGeneroByIdAsync(int id);
        Task<IEnumerable<GeneroResponseDto>> GetAllGenerosAsync();
        Task<GeneroResponseDto> CreateGeneroAsync(CrearGeneroRequestDto dto);
        Task<GeneroResponseDto> UpdateGeneroAsync(int id, CrearGeneroRequestDto dto);
        Task<bool> DeleteGeneroAsync(int id);
    }
}
