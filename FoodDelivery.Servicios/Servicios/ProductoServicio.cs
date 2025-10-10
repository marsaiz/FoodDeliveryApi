using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;

namespace FoodDelivery.Servicios.Servicios;

public class ProductoServicio : IProductoServicio
{
    private readonly IProductoRepositorio _productoRepositorio;
    private readonly IEmpresaServicio _empresaServicio;
    private readonly ICategoriaServicio _categoriaServicio; // Repositorio de categorías

    public ProductoServicio(IProductoRepositorio productoRepositorio, IEmpresaServicio empresaServicio, ICategoriaServicio categoriaServicio)
    {
        _productoRepositorio = productoRepositorio;
        _empresaServicio = empresaServicio;
        _categoriaServicio = categoriaServicio;
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
            IdEmpresa = p.IdEmpresa,
            Activo = p.Activo,
            CantidadDisponible = p.CantidadDisponible
        }).ToList();
    }

    public async Task<ProductoDTO> ObtenerProductoPorIdAsync(int idProducto)
    {
        var producto = await _productoRepositorio.ObtenerProductoPorIdAsync(idProducto);
        if (producto == null) return null;
        return new ProductoDTO
        {
            IdProducto = producto.IdProducto,
            NombreProducto = producto.NombreProducto,
            DescripcionProducto = producto.DescripcionProducto,
            PrecioProducto = producto.PrecioProducto,
            ImagenUrl = producto.ImagenUrl,
            IdCategoria = producto.IdCategoria,
            IdEmpresa = producto.IdEmpresa,
            Activo = producto.Activo,
            CantidadDisponible = producto.CantidadDisponible
        };
    }


    public async Task<ProductoDTO> CrearProductoAsync(ProductoCreateDTO ProductoDTO)
    {
        var categoria = await _categoriaServicio.ObtenerCategoriaPorIdAsync(ProductoDTO.IdCategoria, ProductoDTO.IdEmpresa);
        if (categoria == null)
            throw new Exception("La categoría no existe.");
        if (categoria.IdEmpresa != ProductoDTO.IdEmpresa)
            throw new Exception("La categoría no pertenece a la empresa.");

        var empresa = await _empresaServicio.ObtenerEmpresaPorIdAsync(ProductoDTO.IdEmpresa);
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
            IdEmpresa = ProductoDTO.IdEmpresa,
            CantidadDisponible = ProductoDTO.CantidadDisponible
        };

        if (ProductoDTO.CantidadDisponible < 0)
            throw new Exception("La cantidad disponible no puede ser negativa.");
            
        var productoCreado = await _productoRepositorio.CrearProductoAsync(nuevoproducto);
        return new ProductoDTO
        {
            IdProducto = productoCreado.IdProducto,
            NombreProducto = productoCreado.NombreProducto,
            DescripcionProducto = productoCreado.DescripcionProducto,
            PrecioProducto = productoCreado.PrecioProducto,
            ImagenUrl = productoCreado.ImagenUrl,
            IdCategoria = productoCreado.IdCategoria,
            IdEmpresa = productoCreado.IdEmpresa,
            CantidadDisponible = productoCreado.CantidadDisponible
        };
    }

    public async Task<ProductoDTO> ActualizarProductoAsync(int id_producto, Guid id_empresa, ProductoUpdateDTO productoDTO)
    {
        var empresa = await _empresaServicio.ObtenerEmpresaPorIdAsync(id_empresa);
        if (empresa == null)
        {
            throw new Exception("La empresa no existe.");
        }
    var productoExistente = await _productoRepositorio.ObtenerProductoPorIdAsync(id_producto);
        if (productoExistente == null)
            return null;

        productoExistente.NombreProducto = productoDTO.NombreProducto;
        productoExistente.DescripcionProducto = productoDTO.DescripcionProducto;
        productoExistente.PrecioProducto = productoDTO.PrecioProducto;
        productoExistente.ImagenUrl = productoDTO.ImagenUrl;
        productoExistente.Activo = productoDTO.Activo;
        productoExistente.CantidadDisponible = productoDTO.CantidadDisponible;
        //productoExistente.IdCategoria = productoDTO.IdCategoria; // No se actualiza la categoría

        var productoActualizado = await _productoRepositorio.ActualizarProductoAsync(productoExistente);
        return new ProductoDTO
        {
            IdProducto = productoActualizado.IdProducto,
            NombreProducto = productoActualizado.NombreProducto,
            DescripcionProducto = productoActualizado.DescripcionProducto,
            PrecioProducto = productoActualizado.PrecioProducto,
            ImagenUrl = productoActualizado.ImagenUrl,
            IdCategoria = productoActualizado.IdCategoria,
            IdEmpresa = productoActualizado.IdEmpresa,
            Activo = productoActualizado.Activo,
            CantidadDisponible = productoActualizado.CantidadDisponible
        };
    }

    public async Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa)
    {
        return await _productoRepositorio.EliminarProductoAsync(idProducto, idEmpresa);
    }

    public async Task<List<ProductoDTO>> ObtenerTodosAsync()
    {
        var productos = await _productoRepositorio.ObtenerTodosAsync();
        return productos.Select(p => new ProductoDTO
        {
            IdProducto = p.IdProducto,
            NombreProducto = p.NombreProducto,
            DescripcionProducto = p.DescripcionProducto,
            PrecioProducto = p.PrecioProducto,
            ImagenUrl = p.ImagenUrl,
            IdCategoria = p.IdCategoria,
            IdEmpresa = p.IdEmpresa,
            Activo = p.Activo,
            CantidadDisponible = p.CantidadDisponible
        }).ToList();
    }
}
