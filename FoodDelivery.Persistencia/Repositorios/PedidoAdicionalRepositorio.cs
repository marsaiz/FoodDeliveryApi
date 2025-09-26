using FoodDelivery.Persistencia.Interfaces;
using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class PedidoAdicionalRepositorio : IPedidoAdicionalesRepositorio
{
    private readonly FoodDeliveryDbContext _context;

    public PedidoAdicionalRepositorio(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<List<PedidoAdicionales>> ObtenerPedidoAdicional(int idAdicional, int idDetallePedido)
    {
        return await _context.PedidoAdicionales
            .Where(pa => pa.IdAdicional == idAdicional && pa.IdDetallePedido == idDetallePedido)
            .ToListAsync();
    }

    public async Task<PedidoAdicionales> ActualizarPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales)
    {
        _context.Entry(pedidoAdicionales).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return pedidoAdicionales;
    }

    public async Task<PedidoAdicionales> CrearPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales)
    {
        await _context.PedidoAdicionales.AddAsync(pedidoAdicionales);
        await _context.SaveChangesAsync();
        return pedidoAdicionales;
    }

    public async Task<bool> EliminarAsync(int idDetallePedido, int idAdicional)
    {
        var pedidoAdicional = await _context.PedidoAdicionales
            .Where(pa => pa.IdDetallePedido == idDetallePedido && pa.IdAdicional == idAdicional)
            .FirstOrDefaultAsync();

        if (pedidoAdicional != null)
        {
            _context.PedidoAdicionales.Remove(pedidoAdicional);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
