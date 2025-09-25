using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IPedidoServicio
{
    Task<PedidoDTO> ObtenerPedidoPorIdAsync(int idPedido, Guid idCliente, Guid idEmpresa);
    Task<List<PedidoDTO>> ObtenerPedidosPorClienteAsync(Guid idCliente);
    Task<List<PedidoDTO>> ObtenerPedidosPorEmpresaAsync(Guid idEmpresa);
    Task<PedidoDTO> CrearPedidoAsync(PedidoCreateDTO nuevoPedido);
    Task<PedidoDTO> ActualizarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa, PedidoUpdateDTO pedidoActualizado);
    Task<bool> EliminarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa);
}
