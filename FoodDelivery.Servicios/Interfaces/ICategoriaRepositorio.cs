using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces;

public interface ICategoriaRepositorio
{
    Task<Categoria> CrearCategoriaAsync(Categoria categoria);
    Task<Categoria> ActualizarCategoriaAsync(Categoria categoria);
    Task<bool> EliminarCategoriaAsync(int idCategoria, Guid idEmpresa);
    Task<Categoria> ObtenerCategoriaPorIdAsync(int idCategoria, Guid idEmpresa);
    Task<List<Categoria>> ObtenerCategoriasPorEmpresaAsync(Guid idEmpresa);
}
