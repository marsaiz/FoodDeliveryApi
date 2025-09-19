using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        Task<Cliente> CrearClienteAsync(ClienteDTO cliente);
        Task<Cliente> ActualizarClienteAsync(ClienteDTO cliente);
        Task<bool> EliminarClienteAsync(Guid idCliente);
        Task<Cliente> ObtenerClientePorIdAsync(Guid idCliente);
        Task<List<Cliente>> ObtenerClientesAsync();
    }
}