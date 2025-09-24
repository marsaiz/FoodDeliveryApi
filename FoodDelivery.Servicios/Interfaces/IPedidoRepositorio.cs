using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<Pedido> ObtenerPedidoPorIdAsync(int idPedido, Guid idCliente, Guid idEmpresa);
        Task<List<Pedido>> ObtenerPedidosPorClienteAsync(Guid idCliente);
        Task<List<Pedido>> ObtenerPedidosPorEmpresaAsync(Guid idEmpresa);
        Task<Pedido> CrearPedidoAsync(Pedido nuevoPedido);
        Task<Pedido> ActualizarPedidoAsync(Pedido pedido);
        Task<bool> EliminarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa);
    }
}