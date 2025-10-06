using FoodDelivery.Persistencia.Interfaces;
using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class DetallePedidoRepositorio : IDetallePedidoRepositorio
{
    private readonly FoodDeliveryDbContext _dbContext;

    public DetallePedidoRepositorio(FoodDeliveryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DetallePedido> CrearDetallePedidoAsync(DetallePedido detallePedido)
    {
        await _dbContext.DetallePedidos.AddAsync(detallePedido);
        await _dbContext.SaveChangesAsync();
        return detallePedido;
    }
    public async Task<DetallePedido> ActualizarDetallePedidoAsync(DetallePedido detallePedido)
    {
        _dbContext.Entry(detallePedido).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return detallePedido;
    }
    public async Task<bool> EliminarDetallePedidoAsync(int idDetallePedido)
    {
        var detallePedido = await _dbContext.DetallePedidos.FindAsync(idDetallePedido);
        if (detallePedido != null)
        {
            _dbContext.DetallePedidos.Remove(detallePedido);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<DetallePedido>> ObtenerDetallePorPedidoAsync(int IdPedido)
    {
        return await _dbContext.DetallePedidos
            .Where(dp => dp.IdPedido == IdPedido)
            .ToListAsync();
    }
    public async Task<IEnumerable<DetallePedido>> ObtenerTodosAsync()
    {
        return await _dbContext.DetallePedidos.ToListAsync(); // Devuelve List, pero la interfaz acepta IEnumerable
    }
}