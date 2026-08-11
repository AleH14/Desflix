using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MVCPeliculas.Data
{
    public class PeliculasDbContextFactory : IDesignTimeDbContextFactory<PeliculasDbContext>
    {
        public PeliculasDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\MVCPeliculas"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PeliculasDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new PeliculasDbContext(optionsBuilder.Options);
        }
    }
}
