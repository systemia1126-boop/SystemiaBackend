namespace Systemia.Entidades.Configuracion
{
    public class EVista
    {
        public int IdVista { get; set; }
        public int IdMenu { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Ruta { get; set; } = string.Empty;
        public string? Icono { get; set; }
    }
}
