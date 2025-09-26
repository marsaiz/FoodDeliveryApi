using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;

public interface IPedidoAdicionalesRepositorio
{
    Task<List<PedidoAdicionales>> ObtenerPedidoAdicional(int idAdicional, int idDetallePedido);
    Task<PedidoAdicionales> ActualizarPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales);
    Task<PedidoAdicionales> CrearPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales);
    Task<bool> EliminarAsync(int idDetallePedido, int idAdicional);
}
