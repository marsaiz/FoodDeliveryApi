using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FoodDelivery.Persistencia.Repositorios.Implementaciones
{
    public class AdicionalRepository : IAdicionalRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public AdicionalRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Adicional>> GetAllAsync()
        {
            return await _context.Adicionales.ToListAsync();
        }

        public async Task<Adicional> GetByIdAsync(int id)
        {
            return await _context.Adicionales.FindAsync(id);
        }

        public async Task AddAsync(Adicional adicional)
        {
            await _context.Adicionales.AddAsync(adicional);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Adicional adicional)
        {
            _context.Adicionales.Update(adicional);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var adicional = await _context.Adicionales.FindAsync(id);
            if (adicional != null)
            {
                _context.Adicionales.Remove(adicional);
                await _context.SaveChangesAsync();
            }
        }
    }
}