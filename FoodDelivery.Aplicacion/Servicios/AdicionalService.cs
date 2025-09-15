using FoodDelivery.Domain.Modelos;
using FoodDelivery.Domain.Servicios;
using FoodDelivery.Persistencia.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FoodDelivery.Aplicacion.DTOs;
using AutoMapper;

namespace FoodDelivery.Aplicacion.Servicios
{
    public class AdicionalService : IAdicionalService
    {
        private readonly IMapper _mapper;
        private readonly IAdicionalRepository _adicionalRepository;

        public AdicionalService(IAdicionalRepository adicionalRepository, IMapper mapper)
        {
            _adicionalRepository = adicionalRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Adicional>> GetAllAsync()
        {
            return await _adicionalRepository.GetAllAsync();
        }

        public async Task<Adicional> GetByIdAsync(int id)
        {
            return await _adicionalRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(AdicionalDTO adicionalDTO)
        {
            var adicional = _mapper.Map<Adicional>(adicionalDTO);
            await _adicionalRepository.AddAsync(adicional);
        }

        public async Task UpdateAsync(AdicionalDTO adicional)
        {
            var entity = new Adicional
            {
                // Map properties from adicional (AdicionalDTO) to entity (Adicional)
                // Example:
                // Id = adicional.Id,
                // Name = adicional.Name,
                // Price = adicional.Price
                // Add all necessary property mappings here
            };

            await _adicionalRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _adicionalRepository.DeleteAsync(id);
        }

        public Task AddAsync(Adicional adicional)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Adicional adicional)
        {
            throw new NotImplementedException();
        }
    }
}