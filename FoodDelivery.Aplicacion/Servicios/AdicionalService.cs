using FoodDelivery.Domain.Modelos;
using FoodDelivery.Domain.Servicios;
using FoodDelivery.Persistencia.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FoodDelivery.Aplicacion.Servicios
{
    public class AdicionalService : IAdicionalService
    {
        private readonly IAdicionalRepository _adicionalRepository;

        public AdicionalService(IAdicionalRepository adicionalRepository)
        {
            _adicionalRepository = adicionalRepository;
        }

        public async Task<IEnumerable<Adicional>> GetAllAsync()
        {
            return await _adicionalRepository.GetAllAsync();
        }

        public async Task<Adicional> GetByIdAsync(int id)
        {
            return await _adicionalRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Adicional adicional)
        {
            await _adicionalRepository.AddAsync(adicional);
        }

        public async Task UpdateAsync(Adicional adicional)
        {
            await _adicionalRepository.UpdateAsync(adicional);
        }

        public async Task DeleteAsync(int id)
        {
            await _adicionalRepository.DeleteAsync(id);
        }
    }
}