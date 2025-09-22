using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class AdicionalServicio : IAdicionalServicio
{
    private readonly IAdicionalRepositorio _adicionalRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio; // Asegúrate de tener este repositorio
    
    public AdicionalServicio(IAdicionalRepositorio adicionalRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
        _adicionalRepositorio = adicionalRepositorio;
        _empresaRepositorio = empresaRepositorio;
    }
    public async Task<Adicional> CrearAdicionalAsync(AdicionalCreateDTO adicionalDto)
    {
        // Validar si la empresa existe
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(adicionalDto.IdEmpresa);
        if (empresa == null)
            throw new Exception("La empresa especificada no existe.");
        Console.WriteLine($"Creando adicional para empresa: {adicionalDto.IdEmpresa}");
    
        var nuevoAdicional = new Adicional
        {
            // IdAdicional = new int(), // [DatabaseGenerated] se encarga de esto en el modelo
            NombreAdicional = adicionalDto.NombreAdicional,
            PrecioAdicional = adicionalDto.PrecioAdicional ?? 0, // Asignar 0 si es null
            IdEmpresa = adicionalDto.IdEmpresa
        };
        return await _adicionalRepositorio.CrearAdicionalAsync(nuevoAdicional);
    }

    public async Task<Adicional> ActualizarAdicionalAsync(int idAdicional, Guid idEmpresa, AdicionalUpdateDTO adicionalDto)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);
        if (empresa == null)
            throw new Exception("La empresa especificada no existe.");

        // 1. Obtener la entidad existente desde el repositorio (y la base de datos)
        var adicionalExistente = await _adicionalRepositorio.ObtenerAdicionalPorIdAsync(idAdicional, idEmpresa);
        if (adicionalExistente == null)
            return null; // O lanzar una excepción si prefieres
        
            //IdAdicional = adicionalDto.IdAdicional,
            adicionalExistente.NombreAdicional = adicionalDto.NombreAdicional;
            adicionalExistente.PrecioAdicional = adicionalDto.PrecioAdicional ?? 0; // Asignar 0 si es null

        return await _adicionalRepositorio.ActualizarAdicionalAsync(adicionalExistente);
    }
    public async Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa)
    {
        return await _adicionalRepositorio.EliminarAdicionalAsync(idAdicional, idEmpresa);
    }

    public async Task<Adicional> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa)
    {
        return await _adicionalRepositorio.ObtenerAdicionalPorIdAsync(idAdicional, idEmpresa);
    }
    public async Task<List<Adicional>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa)
    {
        return await _adicionalRepositorio.ObtenerAdicionalesPorEmpresaAsync(idEmpresa);
    }
}
