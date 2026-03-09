using CFA.Clientes.Api.Domain.Entities;

namespace CFA.Clientes.Api.Application.Ports
{
    public interface ITelefonoRepository
    {
        Task<int> CrearTelefono(Telefono telefono);

        Task<List<Telefono>> ObtenerTelefonosCliente(int clienteId);

        Task EliminarTelefono(int telefonoId);
    }
}
