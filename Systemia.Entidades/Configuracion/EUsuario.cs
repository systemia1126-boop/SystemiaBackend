namespace Systemia.Entidades.Configuracion
{
    public class EUsuario
    {
        public int IdUsuario { get; set; }
        public int IdTercero { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public bool IsEstado { get; set; } = true;
        public int IdTipoEstado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int IdUsuarioC { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? IdUsuarioA { get; set; }
    }
}
