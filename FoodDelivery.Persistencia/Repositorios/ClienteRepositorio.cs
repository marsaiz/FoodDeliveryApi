using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public ClienteRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente> CrearClienteAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> ActualizarClienteAsync(Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<bool> EliminarClienteAsync(Guid idCliente)
    {
        var cliente = await _context.Clientes
        .Where(c => c.IdCliente == idCliente)
        .FirstOrDefaultAsync();

        if (cliente == null) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Cliente> ObtenerClientePorIdAsync(Guid idCliente)
    {
        return await _context.Clientes
        .Where(c => c.IdCliente == idCliente)
        .FirstOrDefaultAsync();
    }

    public async Task<List<Cliente>> ObtenerClientesAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<bool> CambiarPasswordAsync(Guid idCliente, string nuevoPasswordHash)
    {
        var cliente = await _context.Clientes
            .Where(c => c.IdCliente == idCliente)
            .FirstOrDefaultAsync();

        if (cliente == null) return false;

        cliente.PasswordHash = nuevoPasswordHash;
        _context.Entry(cliente).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Cliente> ObtenerClientePorUsuarioAsync(string usuario)
    {
        return await _context.Clientes
            .Where(c => c.Usuario == usuario)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExisteClienteConEmailAsync(string email)
    {
        return await _context.Clientes.AnyAsync(c => c.EmailCliente == email);
    }
    public async Task<Cliente> ObtenerClientePorEmailAsync(string email)
    {
        return await _context.Clientes
            .Where(c => c.EmailCliente == email)
            .FirstOrDefaultAsync();
    }
}