using CFA.Clientes.Api.Domain.Entities;

namespace CFA.Clientes.Api.Application.Ports
{
    public interface IClienteRepository
    {
        Task<int> CrearCliente(Cliente cliente);

        Task<List<Cliente>> ObtenerClientes();

        Task<Cliente?> ObtenerPorDocumento(long documento);

        Task ActualizarCliente(Cliente cliente);

        Task EliminarCliente(int codigo);


        Task<Cliente> ObtenerPorId(int i);
    }
}
