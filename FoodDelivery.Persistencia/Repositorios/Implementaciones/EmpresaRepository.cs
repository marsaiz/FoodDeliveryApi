using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Persistencia.Repositorios.Implementaciones
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public EmpresaRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<Empresa> GetByIdAsync(Guid id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task AddAsync(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }
    }
}