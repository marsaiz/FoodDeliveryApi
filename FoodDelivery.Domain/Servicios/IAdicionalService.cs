using FoodDelivery.Domain.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FoodDelivery.Domain.Servicios
{
    public interface IAdicionalService
    {
        Task<IEnumerable<Adicional>> GetAllAsync();
        Task<Adicional> GetByIdAsync(int id);
        Task AddAsync (Adicional adicional);
        Task UpdateAsync(Adicional adicional);
        Task DeleteAsync(int id);
    }
}