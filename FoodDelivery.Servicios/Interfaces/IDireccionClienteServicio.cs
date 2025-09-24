using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IDireccionClienteServicio
{
    Task<DireccionCliente> CrearDireccionClienteAsync(DireccionClienteCreateDTO direccionDto);
    Task<DireccionCliente> ActualizarDireccionClienteAsync(int idDireccionCliente, Guid idCliente, DireccionClienteUpdateDTO direccionDto);
    Task<bool> EliminarDireccionClienteAsync(int idDireccion, Guid idCliente); // Retorna true si se eliminó, false si no se encontró
    Task<DireccionClienteDTO> ObtenerDireccionClientePorIdAsync(int idDireccion, Guid idCliente); // Uso dos parametros para identificar la direccion por cliente
    Task<List<DireccionClienteDTO>> ObtenerDireccionesPorClienteAsync(Guid idCliente);
}
