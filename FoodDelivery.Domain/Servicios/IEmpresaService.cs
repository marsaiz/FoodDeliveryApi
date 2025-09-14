using FoodDelivery.Domain.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FoodDelivery.Domain.Servicios
{
    public interface IEmpresaService
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa> GetByIdAsync(Guid id);
        Task AddAsync(Empresa empresa);
        Task UpdateAsync(Empresa empresa);
        Task DeleteAsync(Guid id);
    }
}