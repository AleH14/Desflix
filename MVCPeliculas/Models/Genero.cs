namespace MVCPeliculas.Models
{
    /// <summary>
    /// DEPRECATED: Use MVCPeliculas.Core.Entities.Genero instead.
    /// This class is kept for backward compatibility only.
    /// The DbContext now uses Core.Entities.Genero as the EF Core model.
    /// </summary>
    [Obsolete("Use MVCPeliculas.Core.Entities.Genero instead", false)]
    public class Genero
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
