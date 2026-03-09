using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Domain.Entities;
using CFA.Clientes.Api.Domain.Helpers;
using CFA.Clientes.Api.Application.DTOs;
using CFA.Clientes.Api.Domain.Enums;

namespace CFA.Clientes.Api.Application.UseCases;

public class ClienteUseCase
{
    private readonly IClienteRepository _repository;

    public ClienteUseCase(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CrearCliente(ClienteRequestDto dto)
    {
        if (!EmailHelper.EsValido(dto.Email))
            throw new Exception("Email inválido");

        int edad = EdadHelper.CalcularEdad(dto.FechaNacimiento);

        var tipoDocumento = Enum.Parse<TipoDocumento>(dto.TipoDocumento);

        if (!TipoDocumentoHelper.EsValidoParaEdad(tipoDocumento, edad))
            throw new Exception("Tipo de documento no válido para la edad");

        var cliente = new Cliente
        {
            TipoDocumento = dto.TipoDocumento,
            NumeroDocumento = dto.NumeroDocumento,
            Nombres = dto.Nombres,
            Apellido1 = dto.Apellido1,
            Apellido2 = dto.Apellido2,
            Genero = dto.Genero,
            FechaNacimiento = dto.FechaNacimiento,
            Email = dto.Email
        };

        return await _repository.CrearCliente(cliente);
    }

    public async Task<List<ClienteResponseDto>> ObtenerClientes()
    {
        var clientes = await _repository.ObtenerClientes();

        return clientes
            .OrderBy(c => c.Nombres)
            .Select(c => new ClienteResponseDto
            {
                Codigo = c.Codigo,
                NombreCompleto = $"{c.Nombres} {c.Apellido1} {c.Apellido2} ",
                NumeroDocumento = c.NumeroDocumento,
                FechaNacimiento = c.FechaNacimiento
            })
            .ToList();
    }

    public async Task ActualizarCliente(int codigo, ClienteRequestDto dto)
    {
        var cliente = new Cliente
        {
            Codigo = codigo,
            TipoDocumento = dto.TipoDocumento,
            NumeroDocumento = dto.NumeroDocumento,
            Nombres = dto.Nombres,
            Apellido1 = dto.Apellido1,
            Apellido2 = dto.Apellido2,
            Genero = dto.Genero,
            FechaNacimiento = dto.FechaNacimiento,
            Email = dto.Email
        };

        await _repository.ActualizarCliente(cliente);
    }

    public async Task EliminarCliente(int codigo)
    {
        await _repository.EliminarCliente(codigo);
    }

    public async Task<List<ClienteBusquedaDto>> BuscarPorNombre(string nombre)
    {
        var clientes = await _repository.ObtenerClientes();

        var resultado = clientes
            .Where(c => (c.Nombres + " " + c.Apellido1)
            .ToLower()
            .Contains(nombre.ToLower()))
            .OrderBy(c => c.Nombres)
            .Select(c => new ClienteBusquedaDto
            {
                Codigo = c.Codigo,
                NombreCompleto = $"{c.Nombres} {c.Apellido1}",
                NumeroDocumento = c.NumeroDocumento
            })
            .ToList();

        return resultado;
    }
    public async Task<List<ClienteBusquedaDto>> BuscarPorDocumento(long numeroDocumento)
    {
        var clientes = await _repository.BuscarPorDocumento(numeroDocumento);

        return clientes.Select(c => new ClienteBusquedaDto
        {
            Codigo = c.Codigo,
            NombreCompleto = $"{c.Nombres} {c.Apellido1}",
            NumeroDocumento = c.NumeroDocumento
        }).ToList();
    }

    public async Task<List<ClienteBusquedaDto>> BuscarPorRangoFechas(DateTime inicio, DateTime fin)
    {
        var clientes = await _repository.BuscarPorRangoFechas(inicio, fin);

        return clientes.Select(c => new ClienteBusquedaDto
        {
            Codigo = c.Codigo,
            NombreCompleto = $"{c.Nombres} {c.Apellido1}",
            NumeroDocumento = c.NumeroDocumento
        }).ToList();
    }

    public async Task<List<ClienteTelefonosDto>> ObtenerClientesMultiplesTelefonos()
    {
        return await _repository.ClientesMultiplesTelefonos();
    }

    public async Task<List<ClienteDireccionesDto>> ObtenerClientesMultiplesDirecciones()
    {
        return await _repository.ClientesMultiplesDirecciones();
    }

    public async Task<ClienteDetalleDto?> ObtenerDetalleCliente(int clienteId)
    {
        return await _repository.ObtenerDetalleCliente(clienteId);
    }
}