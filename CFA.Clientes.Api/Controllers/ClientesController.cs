using Microsoft.AspNetCore.Mvc;
using CFA.Clientes.Api.Application.UseCases;
using CFA.Clientes.Api.Application.DTOs;

namespace CFA.Clientes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteUseCase _useCase;

    public ClientesController(ClienteUseCase useCase)
    {
        _useCase = useCase;
    }

    // POST api/clientes
    [HttpPost]
    public async Task<IActionResult> CrearCliente([FromBody] ClienteRequestDto dto)
    {
        try
        {
            var id = await _useCase.CrearCliente(dto);

            return Created($"api/clientes/{id}", new { codigo = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/clientes
    [HttpGet]
    public async Task<IActionResult> ObtenerClientes()
    {
        var clientes = await _useCase.ObtenerClientes();

        return Ok(clientes);
    }

    // PUT api/clientes/{codigo}
    [HttpPut("{codigo}")]
    public async Task<IActionResult> ActualizarCliente(int codigo, [FromBody] ClienteRequestDto dto)
    {
        try
        {
            await _useCase.ActualizarCliente(codigo, dto);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/clientes/{codigo}
    [HttpDelete("{codigo}")]
    public async Task<IActionResult> EliminarCliente(int codigo)
    {
        await _useCase.EliminarCliente(codigo);

        return NoContent();
    }

    [HttpGet("buscar/{nombre}")]
    public async Task<IActionResult> BuscarClientes(string nombre)
    {
        var result = await _useCase.BuscarPorNombre(nombre);

        return Ok(result);
    }

    [HttpGet("documento/{numero}")]
    public async Task<IActionResult> BuscarPorDocumento(long numero)
    {
        var result = await _useCase.BuscarPorDocumento(numero);

        return Ok(result);
    }

    [HttpGet("fecha")]
    public async Task<IActionResult> BuscarPorFecha(
      [FromQuery] DateTime inicio,
      [FromQuery] DateTime fin)
    {
        var result = await _useCase.BuscarPorRangoFechas(inicio, fin);

        return Ok(result);
    }

    [HttpGet("multiples-telefonos")]
    public async Task<IActionResult> ClientesMultiplesTelefonos()
    {
        var result = await _useCase.ObtenerClientesMultiplesTelefonos();

        return Ok(result);
    }

    [HttpGet("multiples-direcciones")]
    public async Task<IActionResult> ClientesMultiplesDirecciones()
    {
        var result = await _useCase.ObtenerClientesMultiplesDirecciones();

        return Ok(result);
    }

    [HttpGet("{id}/detalle")]
    public async Task<IActionResult> ObtenerDetalleCliente(int id)
    {
        var result = await _useCase.ObtenerDetalleCliente(id);

        if (result == null)
            return NotFound("Cliente no encontrado");

        return Ok(result);
    }
}