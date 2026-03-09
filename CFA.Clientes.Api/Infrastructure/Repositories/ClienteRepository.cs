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

        var sql = @"INSERT INTO clientes
                   (tipo_documento, numero_documento, nombres, apellido1, apellido2, genero, fecha_nacimiento, email)
                   VALUES
                   (@TipoDocumento, @NumeroDocumento, @Nombres, @Apellido1, @Apellido2, @Genero, @FechaNacimiento, @Email)
                   RETURNING codigo";

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
}