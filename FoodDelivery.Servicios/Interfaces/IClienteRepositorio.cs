using FoodDelivery.Domain.Modelos;
namespace FoodDelivery.Servicios.Interfaces;

public interface IClienteRepositorio
{
    Task<Cliente> CrearClienteAsync(Cliente cliente);
    Task<Cliente> ActualizarClienteAsync(Cliente cliente);
    Task<bool> EliminarClienteAsync(Guid idCliente);
    Task<Cliente> ObtenerClientePorIdAsync(Guid idCliente);
    Task<List<Cliente>> ObtenerClientesAsync(Guid idCliente);
}
