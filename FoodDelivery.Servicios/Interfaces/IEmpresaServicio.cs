using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces;

public interface IEmpresaServicio
{
    Task<Empresa> CrearEmpresaAsync(EmpresaDTO empresaDTO);
    Task<Empresa> ActualizarEmpresaAsync(EmpresaDTO empresaDTO);
    Task<bool> EliminarEmpresaAsync(Guid idEmpresa);
    Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa);
    Task<List<Empresa>> ObtenerEmpresasAsync();
}
