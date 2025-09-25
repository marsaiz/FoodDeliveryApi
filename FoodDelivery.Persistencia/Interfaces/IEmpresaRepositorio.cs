using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;
public interface IEmpresaRepositorio
{
    Task<List<Empresa>> ObtenerEmpresasAsync();
    Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa);
    Task CrearEmpresaAsync(Empresa empresa);
    Task ActualizarEmpresaAsync(Empresa empresa);
    Task<bool> EliminarEmpresaAsync(Guid idEmpresa);
    Task<Empresa> ObtenerEmpresaPorUsuarioAsync(string usuario);
    Task<Empresa> ObtenerEmpresaPorEmailAsync(string email);
    Task<bool> CambiarPasswordAsync(Guid idEmpresa, string nuevoPasswordHash);
}
