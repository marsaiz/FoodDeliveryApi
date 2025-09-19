using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IDireccionClienteServicio
{
    Task<DireccionCliente> CrearDireccionClienteAsync(DireccionClienteDTO direccionDto);
    Task<DireccionCliente> ActualizarDireccionClienteAsync(DireccionClienteDTO direccionDto);
    Task<bool> EliminarDireccionClienteAsync(int idDireccion, Guid idCliente); // Retorna true si se eliminó, false si no se encontró
    Task<DireccionCliente> ObtenerDireccionClientePorIdAsync(int idDireccion, Guid idCliente); // Uso dos parametros para identificar la direccion por cliente
    Task<List<DireccionCliente>> ObtenerDireccionesPorClienteAsync(Guid idCliente);
}
