using CFA.Clientes.Api.Application.DTOs;
using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Domain.Entities;
using CFA.Clientes.Api.Infrastructure.Persistence;
using Dapper;

namespace CFA.Clientes.Api.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly DbConnectionFactory _factory;

    public ClienteRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CrearCliente(Cliente cliente)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT sp_insertar_cliente(
            @TipoDocumento,
            @NumeroDocumento,
            @Nombres,
            @Apellido1,
            @Apellido2,
            @Genero,
            @FechaNacimiento::date,
            @Email
            );";

        return await connection.ExecuteScalarAsync<int>(sql, cliente);
        
    }

    public async Task<List<Cliente>> ObtenerClientes()
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                codigo AS Codigo,
                tipo_documento AS TipoDocumento,
                numero_documento AS NumeroDocumento,
                nombres AS Nombres,
                apellido1 AS Apellido1,
                apellido2 AS Apellido2,
                genero AS Genero,
                fecha_nacimiento::timestamp  AS FechaNacimiento,
                email AS Email
                FROM clientes";

        var result = await connection.QueryAsync<Cliente>(sql);

        return result.ToList();
    }

    public async Task<Cliente?> ObtenerPorDocumento(long documento)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT * FROM clientes
                    WHERE numero_documento = @documento";

        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { documento });
    }

    public async Task ActualizarCliente(Cliente cliente)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"UPDATE clientes SET
                    tipo_documento = @TipoDocumento,
                    numero_documento = @NumeroDocumento,
                    nombres = @Nombres,
                    apellido1 = @Apellido1,
                    apellido2 = @Apellido2,
                    genero = @Genero,
                    fecha_nacimiento = @FechaNacimiento,
                    email = @Email
                    WHERE codigo = @Codigo";

        await connection.ExecuteAsync(sql, cliente);
    }

    public async Task EliminarCliente(int codigo)
    {
        using var connection = _factory.CreateConnection();

        var sql = "DELETE FROM clientes WHERE codigo = @codigo";

        await connection.ExecuteAsync(sql, new { codigo });
    }

    public async Task<Cliente?> ObtenerPorId(int id)
    {
        using var connection = _factory.CreateConnection();

        var sql = "SELECT * FROM clientes WHERE codigo = @id";

        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { id });
    }

    public async Task<List<Cliente>> BuscarPorDocumento(long numeroDocumento)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                codigo AS Codigo,
                tipo_documento AS TipoDocumento,
                numero_documento AS NumeroDocumento,
                nombres AS Nombres,
                apellido1 AS Apellido1,
                apellido2 AS Apellido2,
                genero AS Genero,
                fecha_nacimiento::timestamp AS FechaNacimiento,
                email AS Email
                FROM clientes
                WHERE numero_documento = @numeroDocumento
                ORDER BY numero_documento DESC";

        var result = await connection.QueryAsync<Cliente>(sql, new { numeroDocumento });

        return result.ToList();
    }

    public async Task<List<Cliente>> BuscarPorRangoFechas(DateTime inicio, DateTime fin)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                codigo AS Codigo,
                tipo_documento AS TipoDocumento,
                numero_documento AS NumeroDocumento,
                nombres AS Nombres,
                apellido1 AS Apellido1,
                apellido2 AS Apellido2,
                genero AS Genero,
                fecha_nacimiento::timestamp AS FechaNacimiento,
                email AS Email
                FROM clientes
                WHERE fecha_nacimiento BETWEEN @inicio AND @fin
                ORDER BY fecha_nacimiento ASC";

        var result = await connection.QueryAsync<Cliente>(sql, new { inicio, fin });

        return result.ToList();
    }

    public async Task<List<ClienteTelefonosDto>> ClientesMultiplesTelefonos()
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                c.nombres || ' ' || c.apellido1 AS NombreCompleto,
                COUNT(t.id) AS CantidadTelefonos
                FROM clientes c
                JOIN telefonos t
                ON c.codigo = t.cliente_codigo
                GROUP BY c.codigo
                HAVING COUNT(t.id) > 1";

        var result = await connection.QueryAsync<ClienteTelefonosDto>(sql);

        return result.ToList();
    }

    public async Task<List<ClienteDireccionesDto>> ClientesMultiplesDirecciones()
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                c.nombres || ' ' || c.apellido1 AS NombreCompleto,
                MIN(d.direccion) AS Direccion
                FROM clientes c
                JOIN direcciones d
                ON c.codigo = d.cliente_codigo
                GROUP BY c.codigo
                HAVING COUNT(d.id) > 1";

        var result = await connection.QueryAsync<ClienteDireccionesDto>(sql);

        return result.ToList();
    }
    public async Task<ClienteDetalleDto?> ObtenerDetalleCliente(int clienteId)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"
    SELECT
        codigo,
        nombres || ' ' || apellido1 AS NombreCompleto,
        numero_documento AS NumeroDocumento,
        email
    FROM clientes
    WHERE codigo = @clienteId;

    SELECT
        id,
        telefono AS Telefono
    FROM telefonos
    WHERE cliente_codigo = @clienteId;

    SELECT
        id,
        direccion AS Direccion
    FROM direcciones
    WHERE cliente_codigo = @clienteId;
    ";

        using var multi = await connection.QueryMultipleAsync(sql, new { clienteId });

        var cliente = await multi.ReadFirstOrDefaultAsync<ClienteDetalleDto>();

        if (cliente == null)
            return null;

        cliente.Telefonos = (await multi.ReadAsync<TelefonoDto>()).ToList();

        cliente.Direcciones = (await multi.ReadAsync<DireccionDto>()).ToList();

        return cliente;
    }

}