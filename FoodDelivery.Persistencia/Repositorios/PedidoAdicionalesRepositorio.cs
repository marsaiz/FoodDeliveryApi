using FoodDelivery.Persistencia.Interfaces;
using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FoodDelivery.Persistencia.Repositorios
{
    public class PedidoAdicionalesRepositorio : IPedidoAdicionalesRepositorio
    {
        private readonly FoodDeliveryDbContext _dbContext;

        public PedidoAdicionalesRepositorio(FoodDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PedidoAdicionales>> ObtenerPedidoAdicional(int idPedido, int idProducto, int idAdicional)
        {
            return await _dbContext.PedidoAdicionales
                .Where(pa => pa.IdPedido == idPedido && pa.IdProducto == idProducto && (idAdicional == 0 || pa.IdAdicional == idAdicional))
                .ToListAsync();
        }

        public async Task<PedidoAdicionales> CrearPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales)
        {
            await _dbContext.PedidoAdicionales.AddAsync(pedidoAdicionales);
            await _dbContext.SaveChangesAsync();
            return pedidoAdicionales;
        }

        public async Task<PedidoAdicionales> ActualizarPedidoAdicionalAsync(PedidoAdicionales pedidoAdicionales)
        {
            _dbContext.Entry(pedidoAdicionales).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return pedidoAdicionales;
        }

        public async Task<bool> EliminarAsync(int idPedido, int idProducto, int idAdicional)
        {
            var entity = await _dbContext.PedidoAdicionales.FirstOrDefaultAsync(pa => pa.IdPedido == idPedido && pa.IdProducto == idProducto && pa.IdAdicional == idAdicional);
            if (entity != null)
            {
                _dbContext.PedidoAdicionales.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
