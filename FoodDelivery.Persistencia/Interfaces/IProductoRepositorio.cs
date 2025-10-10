using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Interfaces;
public interface IProductoRepositorio
{
    Task<List<Producto>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa);
    Task<Producto> ObtenerProductoPorIdAsync(int idProducto);
    Task<Producto> CrearProductoAsync(Producto producto);
    Task<Producto> ActualizarProductoAsync(Producto producto);
    Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa);
    Task<List<Producto>> ObtenerTodosAsync();
}
