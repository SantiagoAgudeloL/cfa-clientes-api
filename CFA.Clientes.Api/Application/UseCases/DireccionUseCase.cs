using CFA.Clientes.Api.Application.DTOs;
using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Domain.Entities;

namespace CFA.Clientes.Api.Application.UseCases;

public class DireccionUseCase
{
    private readonly IDireccionRepository _repository;
    private readonly IClienteRepository _clienteRepository;

    public DireccionUseCase(
        IDireccionRepository repository,
        IClienteRepository clienteRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
    }

    public async Task<int> CrearDireccion(int clienteId, DireccionRequestDto dto)
    {
        var cliente = await _clienteRepository.ObtenerPorId(clienteId);

        if (cliente == null)
            throw new Exception("El cliente no existe");

        var direccion = new Direccion
        {
            ClienteCodigo = clienteId,
            DireccionTexto = dto.Direccion
        };

        return await _repository.CrearDireccion(direccion);
    }

    public async Task<List<Direccion>> ObtenerDireccionesCliente(int clienteId)
    {
        return await _repository.ObtenerDireccionesCliente(clienteId);
    }

    public async Task EliminarDireccion(int direccionId)
    {
        await _repository.EliminarDireccion(direccionId);
    }
}