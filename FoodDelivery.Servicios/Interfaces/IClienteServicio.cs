using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        Task<Cliente> CrearClienteAsync(ClienteCreateDTO cliente);
        Task<Cliente> ActualizarClienteAsync(ClienteUpdateDTO cliente);
        Task<bool> EliminarClienteAsync(Guid idCliente);
        Task<Cliente> ObtenerClientePorIdAsync(Guid idCliente);
        Task<List<Cliente>> ObtenerClientesAsync();
    }
}