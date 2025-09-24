using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IPedidoServicio
{
    Task<PedidoDTO> ObtenerPedidoPorIdAsync(int idPedido, Guid idCliente, Guid idEmpresa);
    Task<List<PedidoDTO>> ObtenerPedidosPorClienteAsync(int idPedido, Guid idCliente);
    Task<List<PedidoDTO>> ObtenerPedidosPorEmpresaAsync(int idPedido, Guid idEmpresa);
    Task<Pedido> CrearPedidoAsync(PedidoCreateDTO nuevoPedido);
    Task<Pedido> ActualizarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa, PedidoUpdateDTO pedidoActualizado);
    Task<bool> EliminarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa);
}
