using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class AdicionalRepositorio : IAdicionalRepositorio
{
    private readonly FoodDeliveryContexto _context;

    public AdicionalRepositorio(FoodDeliveryContexto context)
    {
        _context = context;
    }
    public async Task<Adicional> CrearAdicionalAsync(Adicional adicional)
    {
        await _context.Adicionales.AddAsync(adicional);
        await _context.SaveChangesAsync();
        return adicional;
    }

    public async Task<Adicional> ActualizarAdicionalAsync(Adicional adicional)
    {
        _context.Entry(adicional).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return adicional;
    }

    public async Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa)
    {
        var adicional = await _context.Adicionales
            .Where(a => a.IdAdicional == idAdicional && a.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();

        if (adicional != null)
        {
            _context.Adicionales.Remove(adicional);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Adicional> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa)
    {
        return await _context.Adicionales
            .Where(a => a.IdAdicional == idAdicional && a.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Adicional>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa)
    {
        return await _context.Adicionales
        .Where(a => a.IdEmpresa == idEmpresa)
        .ToListAsync();
    }
}
