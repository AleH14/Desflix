using Desflix.Core.Entities;

namespace Desflix.Core.Interfaces
{
    /// <summary>
    /// Interfaz genérica base para Repository Pattern
    /// Patrón: Repository Pattern
    /// Define el contrato para operaciones de acceso a datos comunes
    /// </summary>
    public interface IRepository<T> where T : class
    {
        // Lectura
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);
        Task<bool> AnyAsync(Func<T, bool> predicate);

        // Creación
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        // Actualización
        void Update(T entity);

        // Eliminación
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }

    /// <summary>
    /// Interfaz específica para repositorio de Películas
    /// Patrón: Repository Pattern
    /// Define operaciones especializadas para Película
    /// </summary>
    public interface IPeliculaRepository : IRepository<Pelicula>
    {
        // Búsquedas especializadas
        Task<IEnumerable<Pelicula>> GetByGeneroAsync(int generoId);
        Task<IEnumerable<Pelicula>> GetByDirectorAsync(string director);
        Task<IEnumerable<Pelicula>> SearchAsync(string searchTerm);
        Task<IEnumerable<Pelicula>> GetTopRatedAsync(int count = 10);
        Task<IEnumerable<Pelicula>> GetNewestAsync(int count = 10);
        Task<Pelicula?> GetWithGeneroAsync(int id);
    }

    /// <summary>
    /// Interfaz específica para repositorio de Géneros
    /// </summary>
    public interface IGeneroRepository : IRepository<Genero>
    {
        Task<Genero?> GetWithMoviesAsync(int id);
        Task<Genero?> GetByNombreAsync(string nombre);
    }
}
