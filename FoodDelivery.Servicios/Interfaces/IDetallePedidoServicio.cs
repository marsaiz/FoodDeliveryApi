using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Domain.Modelos;
using System.ComponentModel;

namespace FoodDelivery.Servicios.Interfaces;

public interface IDetallePedidoServicio
{
    Task<DetallePedidoDTO> CrearDetallePedidoAsync(DetallePedidoCreateDTO detallePedidoCreateDTO);
    Task<DetallePedidoDTO> ActualizarDetallePedidoAsync(int idPedido, int idProducto, DetallePedidoUpdateDTO detallePedidoUpdateDTO);
    Task<bool> EliminarDetallePedidoAsync(int idPedido, int idProducto);
    Task<DetallePedidoDTO> ObtenerDetallePedidoPorIdAsync(int idPedido, int idProducto);
    Task<List<DetallePedidoDTO>> ObtenerDetallesPorPedidoAsync(int idPedido);
}
