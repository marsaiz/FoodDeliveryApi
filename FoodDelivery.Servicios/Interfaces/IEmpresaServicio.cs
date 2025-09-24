using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IEmpresaServicio
{
    Task<Empresa> CrearEmpresaAsync(EmpresaCreateDTO empresaDTO);
    Task<Empresa> ActualizarEmpresaAsync(EmpresaUpdateDTO empresaDTO);
    Task<bool> EliminarEmpresaAsync(Guid idEmpresa);
    Task<EmpresaDTO> ObtenerEmpresaPorIdAsync(Guid idEmpresa);
    Task<List<EmpresaDTO>> ObtenerEmpresasAsync();
    Task<EmpresaDTO> ObtenerEmpresaPorUsuarioAsync(string usuario);
    Task<EmpresaDTO> ObtenerEmpresaPorEmailAsync(string email);
    Task<bool> CambiarPasswordAsync(Guid idEmpresa, EmpresaChangePasswordDTO dto);
}
