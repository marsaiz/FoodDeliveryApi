using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class ProductoServicio : IProductoServicio
{
    private readonly IProductoRepositorio _productoRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public ProductoServicio(IProductoRepositorio productoRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
        _productoRepositorio = productoRepositorio;
        _empresaRepositorio = empresaRepositorio;
    }

    public async Task<List<Producto>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa)
    {
        return await _productoRepositorio.ObtenerProductosPorEmpresaAsync(idEmpresa);
    }

    public async Task<Producto> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa)
    {
        return await _productoRepositorio.ObtenerProductoPorIdAsync(idProducto, idEmpresa);
    }

    public async Task<Producto> CrearProductoAsync(ProductoCreateDTO ProductoDTO)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(ProductoDTO.IdEmpresa);
        if (empresa == null)
        {
            throw new Exception("La empresa no existe.");
        }

        var nuevoproducto = new Producto
        {
            NombreProducto = ProductoDTO.NombreProducto,
            DescripcionProducto = ProductoDTO.DescripcionProducto,
            PrecioProducto = ProductoDTO.PrecioProducto,
            ImagenUrl = ProductoDTO.ImagenUrl,
        };

        return await _productoRepositorio.CrearProductoAsync(nuevoproducto);
    }

    public async Task<Producto> ActualizarProductoAsync(int id_producto, Guid id_empresa, ProductoUpdateDTO productoDTO)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(id_empresa);
        if (empresa == null)
        {
            throw new Exception("La empresa no existe.");
        }
        var productoExistente = await _productoRepositorio.ObtenerProductoPorIdAsync(id_producto, id_empresa);
        if (productoExistente == null)
            return null;

        productoExistente.NombreProducto = productoDTO.NombreProducto;
        productoExistente.DescripcionProducto = productoDTO.DescripcionProducto;
        productoExistente.PrecioProducto = productoDTO.PrecioProducto;
        productoExistente.ImagenUrl = productoDTO.ImagenUrl;
        //productoExistente.IdCategoria = productoDTO.IdCategoria; // No se actualiza la categoría

        return await _productoRepositorio.ActualizarProductoAsync(productoExistente);
    }

    public async Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa)
    {
        return await _productoRepositorio.EliminarProductoAsync(idProducto, idEmpresa);
    }
}
