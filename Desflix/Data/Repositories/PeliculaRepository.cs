using Microsoft.EntityFrameworkCore;
using Desflix.Core.Entities;
using Desflix.Core.Interfaces;

namespace Desflix.Data.Repositories
{
    /// <summary>
    /// Implementación específica del repositorio para Películas
    /// Patrón: Repository Pattern
    /// Específicamente adaptado para las necesidades de Película
    /// </summary>
    public class PeliculaRepository : Repository<Pelicula>, IPeliculaRepository
    {
        public PeliculaRepository(PeliculasDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pelicula>> GetByGeneroAsync(int generoId)
        {
            return await DbSet
                .Where(p => p.GeneroId == generoId && p.Activo)
                .Include(p => p.Genero)
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pelicula>> GetByDirectorAsync(string director)
        {
            return await DbSet
                .Where(p => p.Director.Contains(director) && p.Activo)
                .Include(p => p.Genero)
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pelicula>> SearchAsync(string searchTerm)
        {
            var term = searchTerm.ToLower();
            return await DbSet
                .Where(p => (p.Titulo.ToLower().Contains(term) ||
                            p.Director.ToLower().Contains(term) ||
                            (p.Sinopsis != null && p.Sinopsis.ToLower().Contains(term))) &&
                            p.Activo)
                .Include(p => p.Genero)
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pelicula>> GetTopRatedAsync(int count = 10)
        {
            return await DbSet
                .Where(p => p.Activo && p.Calificacion.HasValue)
                .Include(p => p.Genero)
                .OrderByDescending(p => p.Calificacion)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pelicula>> GetNewestAsync(int count = 10)
        {
            return await DbSet
                .Where(p => p.Activo)
                .Include(p => p.Genero)
                .OrderByDescending(p => p.FechaCreacion)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Pelicula?> GetWithGeneroAsync(int id)
        {
            return await DbSet
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(p => p.Id == id && p.Activo);
        }
    }
}
