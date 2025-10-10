using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IPedidoAdicionalServicio
{
    Task<List<PedidoAdicionalesDTO>> ObtenerPedidoAdicional(int idPedido, int idProducto, int idAdicional);
    Task<PedidoAdicionalesDTO> ActualizarPedidoAdicionalAsync(PedidoAdicionalesUpdateDTO pedidoAdicionales);
    Task<bool> EliminarAsync(int idPedido, int idProducto, int idAdicional);
    Task<AdicionalDTO> AgregarAdicionalADetallePedidoAsync(int idPedido, int idProducto, int idAdicional, int? mitad, decimal? precioPersonalizado);
}
