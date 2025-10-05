using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;

public interface IDetallePedidoRepositorio
{
    Task<DetallePedido> CrearDetallePedidoAsync(DetallePedido detallePedido);
    Task<DetallePedido> ActualizarDetallePedidoAsync(DetallePedido detallePedido);
    Task<bool> EliminarDetallePedidoAsync(int idPedido);
    Task<IEnumerable<DetallePedido>> ObtenerDetallesPorPedidoAsync(int IdPedido);
    Task<IEnumerable<DetallePedido>> ObtenerTodosAsync();
}
