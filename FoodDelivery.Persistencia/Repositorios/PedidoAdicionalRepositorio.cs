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

    public async Task<List<PedidoAdicionales>> ObtenerPedidoAdicional(int idPedido, int idProducto, int idAdicional)
    {
        return await _context.PedidoAdicionales
            .Where(pa => pa.IdPedido == idPedido && pa.IdProducto == idProducto && pa.IdAdicional == idAdicional)
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
            .Where(pa => pa.IdPedido == idDetallePedido && pa.IdAdicional == idAdicional)
            .FirstOrDefaultAsync();

        if (pedidoAdicional != null)
        {
            _context.PedidoAdicionales.Remove(pedidoAdicional);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> EliminarAsync(int idPedido, int idProducto, int idAdicional)
    {
        var pedidoAdicional = await _context.PedidoAdicionales
            .Where(pa => pa.IdPedido == idPedido && pa.IdProducto == idProducto && pa.IdAdicional == idAdicional)
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
