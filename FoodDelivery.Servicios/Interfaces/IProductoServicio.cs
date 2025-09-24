using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface IProductoServicio
{
    Task<List<ProductoDTO>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa);
    Task<ProductoDTO> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa);
    Task<Producto> CrearProductoAsync(ProductoCreateDTO producto);
    Task<Producto> ActualizarProductoAsync(int idProducto, Guid idEmpresa, ProductoUpdateDTO producto);
    Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa);
}
