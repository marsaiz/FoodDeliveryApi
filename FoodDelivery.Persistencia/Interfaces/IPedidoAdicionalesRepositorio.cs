using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;

public interface IPedidoAdicionalesRepositorio
{
    Task<List<PedidoAdicionales>> ObtenerPedidoAdicional(int idPedido, int idProducto, int idAdicional);
    Task<PedidoAdicionales> ActualizarPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales);
    Task<PedidoAdicionales> CrearPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales);
    Task<bool> EliminarAsync(int idPedido, int idProducto, int idAdicional);
}
