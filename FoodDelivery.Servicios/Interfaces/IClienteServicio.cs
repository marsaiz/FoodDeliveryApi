using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        Task<Cliente> CrearClienteAsync(ClienteCreateDTO cliente);
        Task<Cliente> ActualizarClienteAsync(ClienteUpdateDTO cliente);
        Task<bool> EliminarClienteAsync(Guid idCliente);
        Task<ClienteDTO> ObtenerClientePorIdAsync(Guid idCliente);
        Task<List<ClienteDTO>> ObtenerClientesAsync();
        Task<bool> CambiarPasswordAsync(Guid idCliente, ClienteChangePasswordDTO dto);
        Task<ClienteDTO> ObtenerClientePorUsuarioAsync(string usuario);
        Task<ClienteDTO> ObtenerClientePorEmailAsync(string email);
    }
}