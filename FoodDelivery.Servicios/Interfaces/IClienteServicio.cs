
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        Task<ClienteDTO> CrearClienteAsync(ClienteCreateDTO cliente);
        Task<ClienteDTO> ActualizarClienteAsync(ClienteUpdateDTO cliente);
        Task<bool> EliminarClienteAsync(Guid idCliente);
        Task<ClienteDTO> ObtenerClientePorIdAsync(Guid idCliente);
        Task<List<ClienteDTO>> ObtenerClientesAsync();
        Task<bool> CambiarPasswordAsync(Guid idCliente, ClienteChangePasswordDTO dto);
        Task<ClienteDTO> ObtenerClientePorUsuarioAsync(string usuario);
        Task<ClienteDTO> ObtenerClientePorEmailAsync(string email);
    }
}