using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class ProductoServicio : IProductoServicio
{
    private readonly IProductoRepositorio _productoRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly ICategoriaRepositorio _categoriaRepositorio; // Repositorio de categorías

    public ProductoServicio(IProductoRepositorio productoRepositorio, IEmpresaRepositorio empresaRepositorio, ICategoriaRepositorio categoriaRepositorio)
    {
        _productoRepositorio = productoRepositorio;
        _empresaRepositorio = empresaRepositorio;
        _categoriaRepositorio = categoriaRepositorio;
    }

    public async Task<List<ProductoDTO>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa)
    {
        var productos = await _productoRepositorio.ObtenerProductosPorEmpresaAsync(idEmpresa);
        return productos.Select(p => new ProductoDTO
        {
            IdProducto = p.IdProducto,
            NombreProducto = p.NombreProducto,
            DescripcionProducto = p.DescripcionProducto,
            PrecioProducto = p.PrecioProducto,
            ImagenUrl = p.ImagenUrl,
            IdCategoria = p.IdCategoria,
            IdEmpresa = p.IdEmpresa
        }).ToList();
    }

    public async Task<ProductoDTO> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa)
    {
        var producto = await _productoRepositorio.ObtenerProductoPorIdAsync(idProducto, idEmpresa);
        if (producto == null) return null;
        return new ProductoDTO
        {
            IdProducto = producto.IdProducto,
            NombreProducto = producto.NombreProducto,
            DescripcionProducto = producto.DescripcionProducto,
            PrecioProducto = producto.PrecioProducto,
            ImagenUrl = producto.ImagenUrl,
            IdCategoria = producto.IdCategoria,
            IdEmpresa = producto.IdEmpresa
        };
    }

    public async Task<Producto> CrearProductoAsync(ProductoCreateDTO ProductoDTO)
    {
        var categoria = await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(ProductoDTO.IdCategoria, ProductoDTO.IdEmpresa);
        if (categoria == null)
            throw new Exception("La categoría no existe.");
        if (categoria.IdEmpresa != ProductoDTO.IdEmpresa)
            throw new Exception("La categoría no pertenece a la empresa.");

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
