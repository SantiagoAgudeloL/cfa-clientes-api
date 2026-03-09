using Microsoft.AspNetCore.Mvc;
using CFA.Clientes.Api.Application.DTOs;
using CFA.Clientes.Api.Application.UseCases;

namespace CFA.Clientes.Api.Controllers;

[ApiController]
[Route("api")]
public class DireccionesController : ControllerBase
{
    private readonly DireccionUseCase _useCase;

    public DireccionesController(DireccionUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost("clientes/{clienteId}/direcciones")]
    public async Task<IActionResult> CrearDireccion(int clienteId, DireccionRequestDto dto)
    {
        try
        {
            var id = await _useCase.CrearDireccion(clienteId, dto);

            return Ok(new { id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("clientes/{clienteId}/direcciones")]
    public async Task<IActionResult> ObtenerDirecciones(int clienteId)
    {
        var result = await _useCase.ObtenerDireccionesCliente(clienteId);

        return Ok(result);
    }

    [HttpDelete("direcciones/{direccionId}")]
    public async Task<IActionResult> EliminarDireccion(int direccionId)
    {
        await _useCase.EliminarDireccion(direccionId);

        return NoContent();
    }
}