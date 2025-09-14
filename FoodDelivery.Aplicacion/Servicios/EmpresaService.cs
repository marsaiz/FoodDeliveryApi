using FoodDelivery.Domain.Modelos;
using FoodDelivery.Domain.Servicios;
using FoodDelivery.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Aplicacion.Servicios
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _empresaRepository.GetAllAsync();
        }

        public async Task<Empresa> GetByIdAsync(Guid id)
        {
            return await _empresaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Empresa empresa)
        {
            await _empresaRepository.AddAsync(empresa);
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            await _empresaRepository.UpdateAsync(empresa);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _empresaRepository.DeleteAsync(id);
        }
    }
}