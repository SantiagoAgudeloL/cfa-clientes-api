namespace CFA.Clientes.Api.Application.DTOs
{
    public class ClienteResponseDto
    {
        public int Codigo { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public long NumeroDocumento { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}
