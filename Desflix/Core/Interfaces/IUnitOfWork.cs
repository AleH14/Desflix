namespace Desflix.Core.Interfaces
{
    /// <summary>
    /// Interfaz para Unit of Work Pattern
    /// Patrón: Unit of Work
    /// Coordina las transacciones y gestiona múltiples repositorios
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IPeliculaRepository Peliculas { get; }
        IGeneroRepository Generos { get; }

        Task<bool> SaveChangesAsync();
        void BeginTransaction();
        Task CommitAsync();
        void Rollback();
    }
}
