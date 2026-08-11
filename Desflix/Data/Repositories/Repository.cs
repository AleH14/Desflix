using Microsoft.EntityFrameworkCore;
using Desflix.Core.Entities;
using Desflix.Core.Interfaces;

namespace Desflix.Data.Repositories
{
    /// <summary>
    /// Implementación base del Repository Pattern
    /// Patrón: Repository Pattern
    /// Proporciona operaciones CRUD comunes para cualquier entidad
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PeliculasDbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(PeliculasDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate)
        {
            return await Task.FromResult(DbSet.Where(predicate).ToList());
        }

        public virtual async Task<bool> AnyAsync(Func<T, bool> predicate)
        {
            return await Task.FromResult(DbSet.Any(predicate));
        }

        public virtual async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
