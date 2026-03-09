namespace CFA.Clientes.Api.Application.DTOs
{
    public class ClienteBusquedaDto
    {
        public int Codigo { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public long NumeroDocumento { get; set; }
    }
}
