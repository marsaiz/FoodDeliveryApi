using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IProductoServicio
{
    Task<List<Producto>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa);
    Task<Producto> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa);
    Task<Producto> CrearProductoAsync(ProductoCreateDTO producto);
    Task<Producto> ActualizarProductoAsync(int idProducto, Guid idEmpresa, ProductoUpdateDTO producto);
    Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa);
}
