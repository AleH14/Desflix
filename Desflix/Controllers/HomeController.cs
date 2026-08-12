using Microsoft.AspNetCore.Mvc;
using Desflix.Models;
using Desflix.Core.Interfaces;
using System.Diagnostics;

namespace Desflix.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeliculaService _peliculaService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IPeliculaService peliculaService, ILogger<HomeController> logger)
        {
            _peliculaService = peliculaService ?? throw new ArgumentNullException(nameof(peliculaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var peliculas = await _peliculaService.GetAllPeliculasAsync();

                // Obtener películas destacadas (ordenadas por calificación descendente)
                var peliculasDestacadas = peliculas
                    .OrderByDescending(p => p.Calificacion ?? 0)
                    .Take(6)
                    .ToList();

                // Si no hay películas con calificación, tomar las primeras 6
                if (!peliculasDestacadas.Any())
                {
                    peliculasDestacadas = peliculas.Take(6).ToList();
                }

                return View(peliculasDestacadas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Index: {ex.Message}");
                return View(new List<DTOs.Response.PeliculaResponseDto>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
