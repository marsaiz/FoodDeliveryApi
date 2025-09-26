using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IPedidoAdicionalServicio
{
    Task<List<PedidoAdicionalesDTO>> ObtenerPedidoAdicional(int idAdicional, int idDetallePedido);
    Task<PedidoAdicionalesDTO> CrearPedidoAdicionalAsync(PedidoAdicionalesCreateDTO pedidoAdicionales);
    Task<PedidoAdicionalesDTO> ActualizarPedidoAdicionalAsync(PedidoAdicionalesUpdateDTO pedidoAdicionales);
    Task<bool> EliminarAsync(int idDetallePedido, int idAdicional);
}
