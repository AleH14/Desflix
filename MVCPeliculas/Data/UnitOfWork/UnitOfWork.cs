using Microsoft.EntityFrameworkCore.Storage;
using MVCPeliculas.Core.Interfaces;

namespace MVCPeliculas.Data.UnitOfWork
{
    /// <summary>
    /// Implementación del Unit of Work Pattern
    /// Patrón: Unit of Work
    /// Coordina las transacciones y gestiona múltiples repositorios como una unidad
    /// Asegura consistencia transaccional entre múltiples operaciones
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PeliculasDbContext _context;
        private IPeliculaRepository? _peliculaRepository;
        private IGeneroRepository? _generoRepository;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(PeliculasDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Lazy loading de repositorios
        public IPeliculaRepository Peliculas
        {
            get
            {
                if (_peliculaRepository == null)
                {
                    _peliculaRepository = new Repositories.PeliculaRepository(_context);
                }
                return _peliculaRepository;
            }
        }

        public IGeneroRepository Generos
        {
            get
            {
                if (_generoRepository == null)
                {
                    _generoRepository = new Repositories.GeneroRepository(_context);
                }
                return _generoRepository;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving changes: {ex.Message}");
                throw;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction?.CommitAsync()!;
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
