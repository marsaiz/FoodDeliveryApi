using FoodDelivery.Domain.Modelos;
namespace FoodDelivery.Servicios.Interfaces;

public interface IAdicionalServicio
{
    Task<Adicional> CrearAdicionalAsync(AdicionalDTO adicionalDto);
    Task<Adicional> ActualizarAdicionalAsync(AdicionalDTO adicionalDto);
    Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa); // Retorna true si se eliminó, false si no se encontró
    Task<Adicional> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa); // Uso dos parametros para identificar el adicional por empresa
    Task<List<Adicional>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa);
}
