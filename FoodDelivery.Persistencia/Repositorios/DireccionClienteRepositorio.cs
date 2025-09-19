using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class DireccionClienteRepositorio : IDireccionClienteRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public DireccionClienteRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<DireccionCliente> CrearDireccionClienteAsync(DireccionCliente direccion)
    {
        await _context.DireccionesClientes.AddAsync(direccion);
        await _context.SaveChangesAsync();
        return direccion;
    }

    public async Task<DireccionCliente> ActualizarDireccionClienteAsync(DireccionCliente direccion)
    {
        _context.Entry(direccion).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return direccion;
    }

    public async Task<bool> EliminarDireccionClienteAsync(int idDireccion, Guid idCliente)
    {
        var direccion = await _context.DireccionesClientes
            .Where(d => d.IdDireccionCliente == idDireccion && d.IdCliente == idCliente)
            .FirstOrDefaultAsync();

        if (direccion != null)
        {
            _context.DireccionesClientes.Remove(direccion);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<DireccionCliente> ObtenerDireccionClientePorIdAsync(int idDireccion, Guid idCliente)
    {
        return await _context.DireccionesClientes
            .Where(d => d.IdDireccionCliente == idDireccion && d.IdCliente == idCliente)
            .FirstOrDefaultAsync();
    }

    public async Task<List<DireccionCliente>> ObtenerDireccionesPorClienteAsync(Guid idCliente)
    {
        return await _context.DireccionesClientes
        .Where(d => d.IdCliente == idCliente)
        .ToListAsync();
    }
}
