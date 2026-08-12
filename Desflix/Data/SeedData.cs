using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Desflix.Core.Entities;

namespace Desflix.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PeliculasDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PeliculasDbContext>>()))
            {
                if (context.Generos.Any() || context.Peliculas.Any())
                {
                    return; // DB has been seeded
                }
                context.Generos.AddRange(
                    new Genero { Nombre = "Fantasia" },
                    new Genero { Nombre = "Drama" },
                    new Genero { Nombre = "Aventura" }
                );

                context.Peliculas.AddRange(
                    new Pelicula
                    {
                        Titulo = "Harry potter y la piedra filosofal",
                        FechaLanzamiento = DateTime.Parse("2001-11-16"),
                        GeneroId = 1,
                        Precio = 7.55M,
                        Director = "Chris Columbus",
                        Sinopsis = "Un joven mago descubre su herencia mágica y asiste a Hogwarts, donde enfrenta fuerzas oscuras y descubre un misterio sobre la piedra filosofal.",
                        Calificacion = 7.5M
                    },
                    new Pelicula
                    {
                        Titulo = "El señor de los anillos: La comunidad del anillo",
                        FechaLanzamiento = DateTime.Parse("2001-12-10"),
                        GeneroId = 3,
                        Precio = 8.30M,
                        Director = "Peter Jackson",
                        Sinopsis = "Un grupo de compañeros se embarca en una misión épica para destruir un anillo mágico y salvar la Tierra Media de la oscuridad.",
                        Calificacion = 8.8M
                    },
                    new Pelicula
                    {
                        Titulo = "El silencio de los corderos",
                        FechaLanzamiento = DateTime.Parse("1991-02-14"),
                        GeneroId = 2,
                        Precio = 6.25M,
                        Director = "Jonathan Demme",
                        Sinopsis = "Una joven agente del FBI busca la ayuda de un psicópata encarcelado para capturar a un asesino en serie.",
                        Calificacion = 8.6M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
