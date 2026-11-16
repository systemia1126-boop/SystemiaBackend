namespace Systemia.Entidades.Configuracion
{
    public class ERol
    {
        public int IdRol { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int IdEmpresa { get; set; }
    }
}
