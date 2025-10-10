using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoAdicionalesController : ControllerBase
    {
        private readonly IPedidoAdicionalServicio _pedidoAdicionalServicio;

        public PedidoAdicionalesController(IPedidoAdicionalServicio pedidoAdicionalServicio)
        {
            _pedidoAdicionalServicio = pedidoAdicionalServicio;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedidoAdicional([FromBody] PedidoAdicionalesCreateDTO dto)
        {
            // El método CrearPedidoAdicionalAsync no existe en el servicio ni la interfaz.
            // Si necesitas crear un pedido adicional, utiliza AgregarAdicionalADetallePedidoAsync o implementa el método correspondiente.
            return BadRequest("Método no implementado. Utiliza el flujo de agregar adicional a detalle de pedido.");
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarPedidoAdicional([FromBody] PedidoAdicionalesUpdateDTO dto)
        {
            var result = await _pedidoAdicionalServicio.ActualizarPedidoAdicionalAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{idPedido}/{idProducto}/{idAdicional}")]
        public async Task<IActionResult> EliminarPedidoAdicional(int idPedido, int idProducto, int idAdicional)
        {
            var result = await _pedidoAdicionalServicio.EliminarAsync(idPedido, idProducto, idAdicional);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("{idPedido}/{idProducto}")]
        public async Task<IActionResult> ObtenerAdicionalesPorDetalle(int idPedido, int idProducto)
        {
            // Puedes ajustar la firma según tu servicio/repositorio
            var result = await _pedidoAdicionalServicio.ObtenerPedidoAdicional(idPedido, idProducto, 0);
            return Ok(result);
        }
    }
}
