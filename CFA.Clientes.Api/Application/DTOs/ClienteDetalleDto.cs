namespace CFA.Clientes.Api.Application.DTOs
{
    public class ClienteDetalleDto
    {
        public int Codigo { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public long NumeroDocumento { get; set; }

        public string Email { get; set; } = string.Empty;

        public List<TelefonoDto> Telefonos { get; set; } = new();

        public List<DireccionDto> Direcciones { get; set; } = new();
    }

    public class TelefonoDto
    {
        public int Id { get; set; }

        public long Telefono { get; set; }
    }

    public class DireccionDto
    {
        public int Id { get; set; }

        public string Direccion { get; set; } = string.Empty;
    }
}
