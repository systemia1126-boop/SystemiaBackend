namespace Systemia.Entidades.Configuracion
{
    public class EMenu
    {
        public int IdMenu { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Icono { get; set; }
        public int? IdPadre { get; set; }
    }
}
