using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<Pedido> ObtenerPedidoPorIdAsync(int idPedido, Guid idCliente, Guid idEmpresa);
        Task<List<Pedido>> ObtenerPedidosPorClienteAsync(Guid idCliente);
        Task<Pedido> CrearPedidoAsync(Pedido nuevoPedido);
        Task<Pedido> ActualizarPedidoAsync(int idPedido, Pedido pedidoActualizado);
        Task<bool> EliminarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa);
    }
}