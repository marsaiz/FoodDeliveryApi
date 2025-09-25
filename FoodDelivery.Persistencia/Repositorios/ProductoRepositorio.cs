using FoodDelivery.Domain.Modelos;
using FoodDelivery.Persistencia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class ProductoRepositorio : IProductoRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public ProductoRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> ObtenerProductosPorEmpresaAsync(Guid idEmpresa)
    {
        return await _context.Productos
            .Where(p => p.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<Producto> ObtenerProductoPorIdAsync(int idProducto, Guid idEmpresa)
    {
        return await _context.Productos
            .Where(p => p.IdProducto == idProducto && p.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync(); // si encuentra, devuelve el producto, sino null
    }

    public async Task<Producto> CrearProductoAsync(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto> ActualizarProductoAsync(Producto producto)
    {
        _context.Entry(producto).State = EntityState.Modified; // Marca la entidad como modificada, antes de SaveChanges
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> EliminarProductoAsync(int idProducto, Guid idEmpresa)
    {
        var producto = await _context.Productos
            .Where(p => p.IdProducto == idProducto && p.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();

        if (producto != null)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
    }
    return false; // No se encontró el producto
    }
}
