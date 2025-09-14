using FoodDelivery.Domain.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Persistencia.Repositorios
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(Guid id);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Guid id);
    }
}