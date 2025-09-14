using FoodDelivery.Domain.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Servicios
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(Guid id);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Guid id);
    }
}