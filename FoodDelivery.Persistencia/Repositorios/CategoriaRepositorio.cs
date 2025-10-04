using FoodDelivery.Domain.Modelos;
using FoodDelivery.Persistencia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class CategoriaRepositorio : ICategoriaRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public CategoriaRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> CrearCategoriaAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> ActualizarCategoriaAsync(Categoria categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<bool> EliminarCategoriaAsync(int idCategoria, Guid idEmpresa)
    {
        var categoria = await _context.Categorias
            .Where(c => c.IdCategoria == idCategoria && c.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync(); // Devuelve null si no se encuentra

        if (categoria != null) // Si se encuentra, eliminarla
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Categoria?> ObtenerCategoriaPorIdAsync(int idCategoria, Guid idEmpresa)
    {
        return await _context.Categorias
            .Where(c => c.IdCategoria == idCategoria && c.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Categoria>> ObtenerCategoriasPorEmpresaAsync(Guid idEmpresa)
    {
        return await _context.Categorias
        .Where(c => c.IdEmpresa == idEmpresa)
        .ToListAsync();
    }
}
