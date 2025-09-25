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

    [HttpGet("empresa/{idEmpresa}")]
    public async Task<ActionResult<IEnumerable<Pedido>>> GetAllForEmpresa(Guid idEmpresa)
    {
        var pedidos = await _pedidoServicio.ObtenerPedidosPorEmpresaAsync(idEmpresa);
        return Ok(pedidos);
    }

    [HttpGet("cliente/{idCliente}")]
    public async Task<ActionResult<IEnumerable<Pedido>>> GetAllForCliente(Guid idCliente)
    {
        var pedidos = await _pedidoServicio.ObtenerPedidosPorClienteAsync(idCliente);
        return Ok(pedidos);
    }

    [HttpGet("{idPedido}/{idCliente}/{idEmpresa}")]
    public async Task<ActionResult<Pedido>> GetById(int idPedido, Guid idCliente, Guid idEmpresa)
    {
        var pedidoSeleccionado = await _pedidoServicio.ObtenerPedidoPorIdAsync(idPedido, idCliente, idEmpresa);
        if (pedidoSeleccionado == null)
            return NotFound();
        return Ok(pedidoSeleccionado);
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> Create(PedidoCreateDTO pedidoDto)
    {
        var nuevoPedido = await _pedidoServicio.CrearPedidoAsync(pedidoDto);

        var dto = new PedidoDTO
        {
            IdPedido = nuevoPedido.IdPedido,
            FechaHora = nuevoPedido.FechaHora,
            TotalPedido = nuevoPedido.TotalPedido,
            Estado = nuevoPedido.Estado,
            MetodoPago = nuevoPedido.MetodoPago,
            Entrega = nuevoPedido.Entrega,
            IdCliente = nuevoPedido.IdCliente,
            IdEmpresa = nuevoPedido.IdEmpresa
        };
        return CreatedAtAction(nameof(GetById), new { idPedido = nuevoPedido.IdPedido, idCliente = nuevoPedido.IdCliente, idEmpresa = nuevoPedido.IdEmpresa }, dto);
    }

    [HttpPut("{idPedido}/{idCliente}/{idEmpresa}")]
    public async Task<ActionResult> Update(int idPedido, Guid idCliente, Guid idEmpresa, PedidoUpdateDTO pedidoDto)
    {
        try
        {
            var actualizado = await _pedidoServicio.ActualizarPedidoAsync(idPedido, idCliente, idEmpresa, pedidoDto);
            return Ok(actualizado);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{idPedido}/{idCliente}/{idEmpresa}")]
    public async Task<ActionResult> Delete(int idPedido, Guid idCliente, Guid idEmpresa)
    {
        try
        {
            var eliminado = await _pedidoServicio.EliminarPedidoAsync(idPedido, idCliente, idEmpresa);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
