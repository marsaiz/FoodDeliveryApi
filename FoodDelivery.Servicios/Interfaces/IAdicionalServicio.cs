using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IAdicionalServicio
{
    Task<Adicional> CrearAdicionalAsync(AdicionalCreateDTO adicionalDto);
    Task<Adicional> ActualizarAdicionalAsync(int idAdicional, Guid idEmpresa, AdicionalUpdateDTO adicionalUpdateDTO);
    Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa); // Retorna true si se eliminó, false si no se encontró
    Task<Adicional> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa); // Uso dos parametros para identificar el adicional por empresa
    Task<List<Adicional>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa);
}
