using FoodDelivery.Domain.Modelos;
namespace FoodDelivery.Servicios.Interfaces;

public interface IDetallePedidoRepositorio
{
    Task<DetallePedido> CrearDetallePedidoAsync(DetallePedido detallePedido);
    Task<DetallePedido> ActualizarDetallePedidoAsync(DetallePedido detallePedido);
    Task<bool> EliminarDetallePedidoAsync(int idDetallePedido);
    Task<DetallePedido> ObtenerDetallePedidoPorIdAsync(int IdPedido, int IdProducto);
    Task<IEnumerable<DetallePedido>> ObtenerDetallesPorPedidoAsync(int IdPedido);
}
