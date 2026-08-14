using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Desflix.Core.Interfaces;
using Desflix.DTOs.Request;

namespace Desflix.Controllers
{
    /// <summary>
    /// Controlador para gestión de películas
    /// Patrón: MVC Controller
    /// Utiliza Service Layer para lógica de negocio (separación de responsabilidades)
    /// Trabajas con DTOs en lugar de entidades directas
    /// </summary>
    public class PeliculaController : Controller
    {
        private readonly IPeliculaService _peliculaService;
        private readonly IGeneroService _generoService;
        private readonly ILogger<PeliculaController> _logger;

        public PeliculaController(
            IPeliculaService peliculaService,
            IGeneroService generoService,
            ILogger<PeliculaController> logger)
        {
            _peliculaService = peliculaService ?? throw new ArgumentNullException(nameof(peliculaService));
            _generoService = generoService ?? throw new ArgumentNullException(nameof(generoService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: Pelicula/Index
        public async Task<IActionResult> Index(string? searchString)
        {
            try
            {
                ViewData["CurrentFilter"] = searchString;

                IEnumerable<DTOs.Response.PeliculaResponseDto> peliculas;
                if (string.IsNullOrEmpty(searchString))
                {
                    peliculas = await _peliculaService.GetAllPeliculasAsync();
                }
                else
                {
                    peliculas = await _peliculaService.SearchPeliculasAsync(searchString);
                }

                return View(peliculas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Index: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Pelicula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var pelicula = await _peliculaService.GetPeliculaByIdAsync(id.Value);
                if (pelicula == null)
                {
                    return NotFound();
                }

                return View(pelicula);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Details: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Pelicula/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var generos = await _generoService.GetAllGenerosAsync();
                ViewBag.GeneroId = new SelectList(generos, "Id", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Create GET: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Pelicula/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearPeliculaRequestDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _peliculaService.CreatePeliculaAsync(dto);
                    return RedirectToAction(nameof(Index));
                }

                var generos = await _generoService.GetAllGenerosAsync();
                ViewBag.GeneroId = new SelectList(generos, "Id", "Nombre", dto.GeneroId);
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Create POST: {ex.Message}");
                ModelState.AddModelError("", "Error al crear la película. Por favor intente de nuevo.");

                var generos = await _generoService.GetAllGenerosAsync();
                ViewBag.GeneroId = new SelectList(generos, "Id", "Nombre", dto.GeneroId);
                return View(dto);
            }
        }

        // GET: Pelicula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var pelicula = await _peliculaService.GetPeliculaByIdAsync(id.Value);
                if (pelicula == null)
                {
                    return NotFound();
                }

                var generos = await _generoService.GetAllGenerosAsync();
                ViewBag.GeneroId = new SelectList(generos, "Id", "Nombre", pelicula.Genero?.Id);

                // Mapear a DTO para edición
                var editDto = new ActualizarPeliculaRequestDto
                {
                    Id = pelicula.Id,
                    Titulo = pelicula.Titulo,
                    Sinopsis = pelicula.Sinopsis,
                    FechaLanzamiento = pelicula.FechaLanzamiento,
                    Precio = pelicula.Precio,
                    Director = pelicula.Director,
                    UrlImagen = pelicula.UrlImagen,
                    Calificacion = pelicula.Calificacion,
                    GeneroId = pelicula.Genero?.Id ?? 1
                };

                return View(editDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Edit GET: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Pelicula/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ActualizarPeliculaRequestDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _peliculaService.UpdatePeliculaAsync(id.Value, dto);
                    return RedirectToAction(nameof(Index));
                }

                var generos = await _generoService.GetAllGenerosAsync();
                ViewBag.GeneroId = new SelectList(generos, "Id", "Nombre", dto.GeneroId);
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Edit POST: {ex.Message}");
                ModelState.AddModelError("", "Error al actualizar la película. Por favor intente de nuevo.");

                var generos = await _generoService.GetAllGenerosAsync();
                ViewBag.GeneroId = new SelectList(generos, "Id", "Nombre", dto.GeneroId);
                return View(dto);
            }
        }

        // GET: Pelicula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var pelicula = await _peliculaService.GetPeliculaByIdAsync(id.Value);
                if (pelicula == null)
                {
                    return NotFound();
                }

                return View(pelicula);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Delete GET: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                await _peliculaService.DeletePeliculaAsync(id.Value);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en DeleteConfirmed: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
