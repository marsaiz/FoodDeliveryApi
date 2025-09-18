using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces;

public interface IProductoRepositorio
{
    Task<List<Producto>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa);
    Task<Producto> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa);
    Task<Producto> CrearProductoAsync(Producto producto);
    Task<Producto> ActualizarProductoAsync(Producto producto);
    Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa);
}
