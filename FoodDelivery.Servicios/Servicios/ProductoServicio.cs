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
            IdCategoria = ProductoDTO.IdCategoria,
            IdEmpresa = ProductoDTO.IdEmpresa
        };

        return await _productoRepositorio.CrearProductoAsync(nuevoproducto);
    }

    public async Task<Producto> ActualizarProductoAsync(ProductoUpdateDTO productoDTO)
    {
        if (productoDTO.IdProducto == null)
        {
            throw new ArgumentException("IdProducto es requerido para actualizar.");
        }

        var productoExistente = await _productoRepositorio.ObtenerProductoPorIdAsync(productoDTO.IdProducto, productoDTO.IdEmpresa);
        if (productoExistente == null)
        {
            throw new KeyNotFoundException($"Producto con Id {productoDTO.IdProducto} no encontrado.");
        }

        productoExistente.NombreProducto = productoDTO.NombreProducto;
        productoExistente.DescripcionProducto = productoDTO.DescripcionProducto;
        productoExistente.PrecioProducto = productoDTO.PrecioProducto;
        productoExistente.ImagenUrl = productoDTO.ImagenUrl;
        productoExistente.IdCategoria = productoDTO.IdCategoria;

        return await _productoRepositorio.ActualizarProductoAsync(productoExistente);
    }

    public async Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa)
    {
        return await _productoRepositorio.EliminarProductoAsync(idProducto, idEmpresa);
    }
}
