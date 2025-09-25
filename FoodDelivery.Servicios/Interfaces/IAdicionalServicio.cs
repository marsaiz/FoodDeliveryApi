using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IAdicionalServicio
{
    Task<AdicionalDTO> CrearAdicionalAsync(AdicionalCreateDTO adicionalDto);
    Task<AdicionalDTO> ActualizarAdicionalAsync(int idAdicional, Guid idEmpresa, AdicionalUpdateDTO adicionalUpdateDTO);
    Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa); // Retorna true si se eliminó, false si no se encontró
    Task<AdicionalDTO> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa); // Uso dos parametros para identificar el adicional por empresa
    Task<List<AdicionalDTO>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa);
}
