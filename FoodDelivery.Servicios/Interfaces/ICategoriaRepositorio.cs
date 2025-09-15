
using FoodDelivery.Datos.Modelos;
namespace FoodDelivery.Servicios.Interfaces;

public interface ICategoriaRepositorio
{
    Task<Categoria> CrearCategoriaAsync(Categoria categoria);
    Task<Categoria> ActualizarCategoriaAsync(Categoria categoria);
    Task<bool> EliminarCategoriaAsync(int idCategoria, Guid empresaId);
    Task<Categoria> ObtenerCategoriaPorIdAsync(int idCategoria, Guid empresaId);
    Task<IEnumerable<Categoria>> ObtenerCategoriasPorEmpresaAsync(Guid empresaId);
}
