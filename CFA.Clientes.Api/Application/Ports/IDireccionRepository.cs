using CFA.Clientes.Api.Domain.Entities;

namespace CFA.Clientes.Api.Application.Ports
{
    public interface IDireccionRepository
    {
        Task<int> CrearDireccion(Direccion direccion);

        Task<List<Direccion>> ObtenerDireccionesCliente(int clienteId);

        Task EliminarDireccion(int direccionId);
    }
}
