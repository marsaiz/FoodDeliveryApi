using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IEmpresaServicio
{
    Task<Empresa> CrearEmpresaAsync(EmpresaCreateDTO empresaDTO);
    Task<Empresa> ActualizarEmpresaAsync(EmpresaUpdateDTO empresaDTO);
    Task<bool> EliminarEmpresaAsync(Guid idEmpresa);
    Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa);
    Task<List<Empresa>> ObtenerEmpresasAsync();
}
