using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;
public interface IDireccionClienteRepositorio
{
    Task<DireccionCliente> CrearDireccionClienteAsync(DireccionCliente direccion);
    Task<DireccionCliente> ActualizarDireccionClienteAsync(DireccionCliente direccion);
    Task<bool> EliminarDireccionClienteAsync(int idDireccion, Guid idCliente); // Retorna true si se eliminó, false si no se encontró
    Task<DireccionCliente> ObtenerDireccionClientePorIdAsync(int idDireccion, Guid idCliente); // Uso dos parametros para identificar la direccion por cliente
    Task<List<DireccionCliente>> ObtenerDireccionesPorClienteAsync(Guid idCliente);
}
