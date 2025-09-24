using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoServicio _pedidoServicio;

    public PedidoController(IPedidoServicio pedidoServicio)
    {
        _pedidoServicio = pedidoServicio;
    }

    [HttpPost]
    public async Task<IActionResult> CrearPedido([FromBody] PedidoCreateDTO nuevoPedido)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pedidoCreado = await _pedidoServicio.CrearPedidoAsync(nuevoPedido);
        return CreatedAtAction(nameof(ObtenerPedidoPorId), new { id = pedidoCreado.IdPedido }, pedidoCreado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPedidoPorId(int id)
    {
        var pedido = await _pedidoServicio.ObtenerPedidoPorIdAsync(id);
        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    // Otros métodos del controlador...
}
}
