using Microsoft.AspNetCore.Mvc;
using CFA.Clientes.Api.Application.DTOs;
using CFA.Clientes.Api.Application.UseCases;

namespace CFA.Clientes.Api.Controllers;

[ApiController]
[Route("api")]
public class TelefonosController : ControllerBase
{
    private readonly TelefonoUseCase _useCase;

    public TelefonosController(TelefonoUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost("clientes/{clienteId}/telefonos")]
    public async Task<IActionResult> CrearTelefono(int clienteId, TelefonoRequestDto dto)
    {
        try
        {
            var id = await _useCase.CrearTelefono(clienteId, dto);
            return Ok(new { id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("clientes/{clienteId}/telefonos")]
    public async Task<IActionResult> ObtenerTelefonos(int clienteId)
    {
        var result = await _useCase.ObtenerTelefonosCliente(clienteId);

        return Ok(result);
    }

    [HttpDelete("telefonos/{telefonoId}")]
    public async Task<IActionResult> EliminarTelefono(int telefonoId)
    {
        await _useCase.EliminarTelefono(telefonoId);

        return NoContent();
    }
}