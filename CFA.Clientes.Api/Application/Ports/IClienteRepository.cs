using CFA.Clientes.Api.Application.DTOs;
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

        Task<List<Cliente>> BuscarPorDocumento(long codigo);

        Task<List<Cliente>> BuscarPorRangoFechas(DateTime inicio, DateTime fin);

        Task<List<ClienteTelefonosDto>> ClientesMultiplesTelefonos();

        Task<List<ClienteDireccionesDto>> ClientesMultiplesDirecciones();

        Task<ClienteDetalleDto?> ObtenerDetalleCliente(int clienteId);
    }
}
