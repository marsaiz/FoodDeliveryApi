using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class PedidoRepositorio : IPedidoRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public PedidoRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido> CrearPedidoAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task<Pedido> ObtenerPedidoPorIdAsync(int idPedido, Guid idCliente, Guid idEmpresa)
    {
        return await _context.Pedidos
            .Where(p => p.IdPedido == idPedido && p.IdCliente == idCliente && p.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync(); // si encuentra, devuelve el pedido, sino null
    }

    public async Task<List<Pedido>> ObtenerPedidosPorClienteAsync(Guid idCliente)
    {
        return await _context.Pedidos
            .Where(p => p.IdCliente == idCliente)
            .ToListAsync();
    }

    public async Task<List<Pedido>> ObtenerPedidosPorEmpresaAsync(Guid idEmpresa)
    {
        return await _context.Pedidos
            .Where(p => p.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<Pedido> ActualizarPedidoAsync(Pedido pedido)
    {
        _context.Entry(pedido).State = EntityState.Modified; // Marca la entidad como modificada, antes de SaveChanges
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task<bool> EliminarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa)
    {
        var pedido = await _context.Pedidos
            .Where(p => p.IdPedido == idPedido && p.IdCliente == idCliente && p.IdEmpresa == idEmpresa)
            .FirstOrDefaultAsync();

        if (pedido != null)
        {
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
        return false; // No se encontró el pedido
    }
}