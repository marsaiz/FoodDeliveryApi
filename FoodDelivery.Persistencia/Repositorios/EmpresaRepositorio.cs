using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class EmpresaRepositorio : IEmpresaRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public EmpresaRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Empresa>> ObtenerEmpresasAsync()
    {
        return await _context.Empresas.ToListAsync();
    }

    public async Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa)
    {
        return await _context.Empresas
            .Where(e => e.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();
    }

    public async Task CrearEmpresaAsync(Empresa empresa)
    {
        await _context.Empresas.AddAsync(empresa);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarEmpresaAsync(Empresa empresa)
    {
        _context.Entry(empresa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EliminarEmpresaAsync(Guid idEmpresa)
    {
        var empresa = await _context.Empresas
            .Where(e => e.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();

        if (empresa == null) return false;

        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
        return true;
    }
}