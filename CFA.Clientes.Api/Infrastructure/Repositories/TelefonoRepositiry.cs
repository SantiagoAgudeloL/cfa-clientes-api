using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Domain.Entities;
using CFA.Clientes.Api.Infrastructure.Persistence;
using Dapper;

namespace CFA.Clientes.Api.Infrastructure.Repositories;

public class TelefonoRepository : ITelefonoRepository
{
    private readonly DbConnectionFactory _factory;

    public TelefonoRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CrearTelefono(Telefono telefono)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"INSERT INTO telefonos (cliente_codigo, telefono)
                    VALUES (@ClienteCodigo, @TelefonoNumero)
                    RETURNING id";

        return await connection.ExecuteScalarAsync<int>(sql, telefono);
    }

    public async Task<List<Telefono>> ObtenerTelefonosCliente(int clienteId)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                    id AS Id,
                    cliente_codigo AS ClienteCodigo,
                    telefono AS TelefonoNumero
                    FROM telefonos
                    WHERE cliente_codigo = @clienteId";

        var result = await connection.QueryAsync<Telefono>(sql, new { clienteId });

        return result.ToList();
    }

    public async Task EliminarTelefono(int telefonoId)
    {
        using var connection = _factory.CreateConnection();

        var sql = "DELETE FROM telefonos WHERE id = @telefonoId";

        await connection.ExecuteAsync(sql, new { telefonoId });
    }
}