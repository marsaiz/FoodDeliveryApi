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

    [HttpGet("cliente/{idCliente}")]
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
    public async Task<ActionResult<DireccionCliente>> Create(DireccionClienteCreateDTO direccionDto)
    {
        var nuevaDireccion = await _direccionClienteServicio.CrearDireccionClienteAsync(direccionDto);

        var dto = new DireccionClienteDTO
        {
            IdDireccionCliente = nuevaDireccion.IdDireccionCliente,
            Calle = nuevaDireccion.Calle,
            Numero = nuevaDireccion.Numero,
            PisoDepto = nuevaDireccion.PisoDepto,
            Ciudad = nuevaDireccion.Ciudad,
            CodigoPostal = nuevaDireccion.CodigoPostal,
            Referencia = nuevaDireccion.Referencia,
            Latitud = nuevaDireccion.Latitud,
            Longitud = nuevaDireccion.Longitud,
            IdCliente = nuevaDireccion.IdCliente
        };
        return CreatedAtAction(nameof(GetById), new { idDireccion = nuevaDireccion.IdDireccionCliente, idCliente = nuevaDireccion.IdCliente }, dto);
    }

    [HttpPut("{idDireccion}/{idCliente}")]
    public async Task<ActionResult> Update(int idDireccion, Guid idCliente, DireccionClienteUpdateDTO direccionDto)
    {
       var direccionClienteUpdate = new DireccionClienteUpdateDTO
        {
            Calle = direccionDto.Calle,
            Numero = direccionDto.Numero,
            PisoDepto = direccionDto.PisoDepto,
            Ciudad = direccionDto.Ciudad,
            CodigoPostal = direccionDto.CodigoPostal,
            Referencia = direccionDto.Referencia,
            Latitud = direccionDto.Latitud,
            Longitud = direccionDto.Longitud
        };

        var actualizado = await _direccionClienteServicio.ActualizarDireccionClienteAsync(idDireccion, idCliente, direccionClienteUpdate);
        if (actualizado == null)
            return NotFound();

        return NoContent();
    }

    // Método para eliminar una dirección de cliente
    [HttpDelete("{idDireccion}/{idCliente}")]
    public async Task<ActionResult> Delete(int idDireccion, Guid idCliente)
    {
        var resultado = await _direccionClienteServicio.EliminarDireccionClienteAsync(idDireccion, idCliente);
        if (resultado == null)
        {
            return NotFound();
        }
        await _direccionClienteServicio.EliminarDireccionClienteAsync(idDireccion, idCliente);
        return NoContent();
    }
}
