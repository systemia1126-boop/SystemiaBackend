namespace Systemia.Entidades.Configuracion
{
    public class EEmpresa
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? NombreComercial { get; set; }
        public string Nit { get; set; } = string.Empty;
        public byte? DV { get; set; }
        public string? RazonSocial { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int IdMunicipio { get; set; }
        public int IdTipoPersona { get; set; }
    }
}
