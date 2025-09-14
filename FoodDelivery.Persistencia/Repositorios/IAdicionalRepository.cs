using FoodDelivery.Domain.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FoodDelivery.Persistencia.Repositorios
{
    public interface IAdicionalRepository
    {
        Task<IEnumerable<Adicional>> GetAllAsync();
        Task<Adicional> GetByIdAsync(int id);
        Task AddAsync(Adicional adicional);
        Task UpdateAsync(Adicional adicional);
        Task DeleteAsync(int id);
    }
}