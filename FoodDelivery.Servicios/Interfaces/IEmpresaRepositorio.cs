using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces;

public interface IEmpresaRepositorio
{
    Task<List<Empresa>> ObtenerEmpresasAsync();
    Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa);
    Task CrearEmpresaAsync(Empresa empresa);
    Task ActualizarEmpresaAsync(Empresa empresa);
    Task<bool> EliminarEmpresaAsync(Guid idEmpresa);
}
