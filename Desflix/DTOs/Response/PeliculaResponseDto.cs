namespace Desflix.DTOs.Response
{
    /// <summary>
    /// DTO de respuesta para Película
    /// Patrón: Data Transfer Object (DTO)
    /// Estructura de salida separada del modelo de dominio
    /// </summary>
    public class PeliculaResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Sinopsis { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public decimal Precio { get; set; }
        public string Director { get; set; } = string.Empty;
        public string? UrlImagen { get; set; }
        public decimal? Calificacion { get; set; }
        public GeneroResponseDto? Genero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para Género
    /// </summary>
    public class GeneroResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CantidadPeliculas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }

    /// <summary>
    /// DTO de respuesta paginada
    /// </summary>
    public class PaginatedResponseDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
