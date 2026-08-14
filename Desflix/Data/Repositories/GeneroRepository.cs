using Microsoft.EntityFrameworkCore;
using Desflix.Core.Entities;
using Desflix.Core.Interfaces;

namespace Desflix.Data.Repositories
{
    /// <summary>
    /// Implementación específica del repositorio para Géneros
    /// Patrón: Repository Pattern
    /// </summary>
    public class GeneroRepository : Repository<Genero>, IGeneroRepository
    {
        public GeneroRepository(PeliculasDbContext context) : base(context)
        {
        }

        public async Task<Genero?> GetWithMoviesAsync(int id)
        {
            return await DbSet
                .Include(g => g.Peliculas.Where(p => p.Activo))
                .FirstOrDefaultAsync(g => g.Id == id && g.Activo);
        }

        public async Task<Genero?> GetByNombreAsync(string nombre)
        {
            return await DbSet
                .FirstOrDefaultAsync(g => g.Nombre.ToLower() == nombre.ToLower() && g.Activo);
        }

        // Override para GetAllAsync - solo géneros activos
        public override async Task<IEnumerable<Genero>> GetAllAsync()
        {
            return await DbSet
                .Where(g => g.Activo)
                .OrderBy(g => g.Nombre)
                .ToListAsync();
        }
    }
}
