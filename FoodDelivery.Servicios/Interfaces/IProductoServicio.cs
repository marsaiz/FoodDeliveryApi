using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.Interfaces;

public interface IProductoServicio
{
    Task<List<Producto>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa);
    Task<Producto> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa);
    Task<Producto> CrearProductoAsync(ProductoCreateDTO producto, Guid idEmpresa);
    Task ActualizarProductoAsync(ProductoUpdateDTO producto, Guid idEmpresa);
    Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa);
}
