using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;

public interface IAdicionalRepositorio
{
    Task<Adicional> CrearAdicionalAsync(Adicional adicional);
    Task<Adicional> ActualizarAdicionalAsync(Adicional adicional);
    Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa); // Retorna true si se eliminó, false si no se encontró
    Task<Adicional?> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa); // Uso dos parametros para identificar el adicional por empresa
    Task<List<Adicional>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa);
}