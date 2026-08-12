
using Microsoft.EntityFrameworkCore;
using Desflix.Core.Entities;

namespace Desflix.Data
{
    public class PeliculasDbContext : DbContext
    {
        public PeliculasDbContext(DbContextOptions<PeliculasDbContext> options) : base(options)
        {
        }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pelicula>()
                .Property(p => p.Calificacion)
                .HasColumnType("decimal(3,1)");

            modelBuilder.Entity<Pelicula>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(10,2)");
        }
    }
}
