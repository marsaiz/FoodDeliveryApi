using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DireccionClienteController : ControllerBase
{
    private readonly IDireccionClienteServicio _direccionClienteServicio;

    public DireccionClienteController(IDireccionClienteServicio direccionClienteServicio)
    {
        _direccionClienteServicio = direccionClienteServicio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DireccionCliente>>> GetAll(Guid idCliente)
    {
        var direcciones = await _direccionClienteServicio.ObtenerDireccionesPorClienteAsync(idCliente);
        return Ok(direcciones);
    }
    [HttpGet("{idDireccion}/{idCliente}")]
    public async Task<ActionResult<DireccionCliente>> GetById(int idDireccion, Guid idCliente)
    {
        var direccionSeleccionada = await _direccionClienteServicio.ObtenerDireccionClientePorIdAsync(idDireccion, idCliente);
        if (direccionSeleccionada == null)
            return NotFound();
        return Ok(direccionSeleccionada);
    }
    [HttpPost]
    public async Task<ActionResult<DireccionCliente>> Create(DireccionClienteDTO direccionDto)
    {
        var nuevaDireccion = await _direccionClienteServicio.CrearDireccionClienteAsync(direccionDto);
        return CreatedAtAction(nameof(GetById), new { idDireccion = nuevaDireccion.IdDireccionCliente, idCliente = nuevaDireccion.IdCliente }, nuevaDireccion);
    }

    [HttpPut("{idDireccion}")]
    public async Task<ActionResult> Update(int idDireccion, DireccionClienteDTO direccionDto)
    {
        if (idDireccion != direccionDto.IdDireccionCliente)
            return BadRequest();

        var resultado = await _direccionClienteServicio.ActualizarDireccionClienteAsync(direccionDto);
        if (resultado == null)
            return NotFound();

        return NoContent();
    }

    // Método para eliminar una dirección de cliente
    [HttpDelete("{idDireccion}/{idCliente}")]
    public async Task<ActionResult> Delete(int idDireccion, Guid idCliente)
    {
        var resultado = await _direccionClienteServicio.EliminarDireccionClienteAsync(idDireccion, idCliente);
        if (!resultado)
            return NotFound();

        return NoContent();
    }
}
