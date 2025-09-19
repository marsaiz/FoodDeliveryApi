using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class EmpresaServicio : IEmpresaServicio
{
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public EmpresaServicio(IEmpresaRepositorio empresaRepositorio)
    {
        _empresaRepositorio = empresaRepositorio;
    }

    public async Task<Empresa> CrearEmpresaAsync(EmpresaDTO empresaDTO)
    {
        var empresa = new Empresa
        {
            IdEmpresa = Guid.NewGuid(),
            Nombre = empresaDTO.Nombre,
            Direccion = empresaDTO.Direccion,
            Telefono = empresaDTO.Telefono,
            Email = empresaDTO.Email
        };

        await _empresaRepositorio.CrearEmpresaAsync(empresa);
        return empresa;
    }

    public async Task<Empresa> ActualizarEmpresaAsync(EmpresaDTO empresaDTO)
    {
        if (empresaDTO.IdEmpresa == null)
            throw new ArgumentException("El IdEmpresa no puede ser nulo para la actualización.");

        var empresaExistente = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(empresaDTO.IdEmpresa.Value);
        if (empresaExistente == null)
            throw new Exception("Empresa no encontrada.");

        empresaExistente.Nombre = empresaDTO.Nombre;
        empresaExistente.Direccion = empresaDTO.Direccion;
        empresaExistente.Telefono = empresaDTO.Telefono;
        empresaExistente.Email = empresaDTO.Email;

        await _empresaRepositorio.ActualizarEmpresaAsync(empresaExistente);
        return empresaExistente;
    }

    public async Task<bool> EliminarEmpresaAsync(Guid idEmpresa)
    {
        return await _empresaRepositorio.EliminarEmpresaAsync(idEmpresa);
    }

    public async Task<Empresa> ObtenerEmpresaPorIdAsync(Guid idEmpresa)
    {
        return await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);
    }

    public async Task<List<Empresa>> ObtenerEmpresasAsync()
    {
        return await _empresaRepositorio.ObtenerEmpresasAsync();
    }
}
