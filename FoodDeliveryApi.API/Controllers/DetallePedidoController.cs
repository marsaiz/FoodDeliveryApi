using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoServicio _detallePedidoServicio;

        public DetallePedidoController(IDetallePedidoServicio detallePedidoServicio)
        {
            _detallePedidoServicio = detallePedidoServicio;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetallePedido([FromBody] DetallePedidoCreateDTO dto)
        {
            var result = await _detallePedidoServicio.CrearDetallePedidoAsync(dto);
            return Ok(result);
        }

        [HttpPut("{idPedido}/{idProducto}")]
        public async Task<IActionResult> ActualizarDetallePedido(int idPedido, int idProducto, [FromBody] DetallePedidoUpdateDTO dto)
        {
            var result = await _detallePedidoServicio.ActualizarDetallePedidoAsync(idPedido, idProducto, dto);
            return Ok(result);
        }

        [HttpDelete("{idPedido}/{idProducto}")]
        public async Task<IActionResult> EliminarDetallePedido(int idPedido, int idProducto)
        {
            var result = await _detallePedidoServicio.EliminarDetallePedidoAsync(idPedido, idProducto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("{idPedido}/{idProducto}")]
        public async Task<IActionResult> ObtenerDetallePedidoPorId(int idPedido, int idProducto)
        {
            var result = await _detallePedidoServicio.ObtenerDetallePedidoPorIdAsync(idPedido, idProducto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("pedido/{idPedido}")]
        public async Task<IActionResult> ObtenerDetallesPorPedido(int idPedido)
        {
            var result = await _detallePedidoServicio.ObtenerDetallesPorPedidoAsync(idPedido);
            return Ok(result);
        }
    }
}
