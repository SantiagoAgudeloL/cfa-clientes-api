using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Domain.Entities;
using CFA.Clientes.Api.Infrastructure.Persistence;
using Dapper;

namespace CFA.Clientes.Api.Infrastructure.Repositories;

public class DireccionRepository : IDireccionRepository
{
    private readonly DbConnectionFactory _factory;

    public DireccionRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CrearDireccion(Direccion direccion)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"INSERT INTO direcciones (cliente_codigo, direccion)
                    VALUES (@ClienteCodigo, @DireccionTexto)
                    RETURNING id";

        return await connection.ExecuteScalarAsync<int>(sql, direccion);
    }

    public async Task<List<Direccion>> ObtenerDireccionesCliente(int clienteId)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT
                    id AS Id,
                    cliente_codigo AS ClienteCodigo,
                    direccion AS DireccionTexto
                    FROM direcciones
                    WHERE cliente_codigo = @clienteId";

        var result = await connection.QueryAsync<Direccion>(sql, new { clienteId });

        return result.ToList();
    }

    public async Task EliminarDireccion(int direccionId)
    {
        using var connection = _factory.CreateConnection();

        var sql = "DELETE FROM direcciones WHERE id = @direccionId";

        await connection.ExecuteAsync(sql, new { direccionId });
    }
}