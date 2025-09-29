using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Domain.Modelos;
using FoodDelivery.Persistencia.Interfaces;

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
    public async Task<AdicionalDTO> CrearAdicionalAsync(AdicionalCreateDTO adicionalDto)
    {
        // Validar si la empresa existe
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(adicionalDto.IdEmpresa);

        if (empresa == null)
            throw new Exception("La empresa especificada no existe.");
        Console.WriteLine($"Creando adicional para empresa: {adicionalDto.IdEmpresa}"); // Debug log
    
        var nuevoAdicional = new Adicional
        {
            // IdAdicional = new int(), // [DatabaseGenerated] se encarga de esto en el modelo
            NombreAdicional = adicionalDto.NombreAdicional,
            PrecioAdicional = adicionalDto.PrecioAdicional ?? 0, // Asignar 0 si es null
            IdEmpresa = adicionalDto.IdEmpresa
        };
        var adicionalCreado = await _adicionalRepositorio.CrearAdicionalAsync(nuevoAdicional);
        return new AdicionalDTO
        {
            IdAdicional = adicionalCreado.IdAdicional,
            NombreAdicional = adicionalCreado.NombreAdicional,
            PrecioAdicional = adicionalCreado.PrecioAdicional,
            IdEmpresa = adicionalCreado.IdEmpresa
        };
    }

    public async Task<AdicionalDTO> ActualizarAdicionalAsync(int idAdicional, Guid idEmpresa, AdicionalUpdateDTO adicionalDto)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);

        if (empresa == null)
            throw new Exception("La empresa especificada no existe.");

        // 1. Obtener la entidad existente desde el repositorio (y la base de datos)
        var adicionalExistente = await _adicionalRepositorio.ObtenerAdicionalPorIdAsync(idAdicional, idEmpresa);
        if (adicionalExistente == null)
            return null; // O lanzar una excepción si prefieres, ademas permite manejar el error en el controlador con un NotFound 404
        
            adicionalExistente.NombreAdicional = adicionalDto.NombreAdicional;
            adicionalExistente.PrecioAdicional = adicionalDto.PrecioAdicional ?? 0; // Asignar 0 si es null

        var actualizado = await _adicionalRepositorio.ActualizarAdicionalAsync(adicionalExistente);
        return new AdicionalDTO
        {
            IdAdicional = actualizado.IdAdicional,
            NombreAdicional = actualizado.NombreAdicional,
            PrecioAdicional = actualizado.PrecioAdicional,
            IdEmpresa = actualizado.IdEmpresa
        };
    }
    public async Task<bool> EliminarAdicionalAsync(int idAdicional, Guid idEmpresa)
    {
        return await _adicionalRepositorio.EliminarAdicionalAsync(idAdicional, idEmpresa);
    }

    public async Task<AdicionalDTO> ObtenerAdicionalPorIdAsync(int idAdicional, Guid idEmpresa)
    {
        var adicional = await _adicionalRepositorio.ObtenerAdicionalPorIdAsync(idAdicional, idEmpresa);
        if (adicional == null)
            return null;

        return new AdicionalDTO
        {
            IdAdicional = adicional.IdAdicional,
            NombreAdicional = adicional.NombreAdicional,
            PrecioAdicional = adicional.PrecioAdicional,
            IdEmpresa = adicional.IdEmpresa
        };
    }
    public async Task<List<AdicionalDTO>> ObtenerAdicionalesPorEmpresaAsync(Guid idEmpresa)
    {
        var adicionales = await _adicionalRepositorio.ObtenerAdicionalesPorEmpresaAsync(idEmpresa);
        return adicionales.Select(a => new AdicionalDTO
        {
            IdAdicional = a.IdAdicional,
            NombreAdicional = a.NombreAdicional,
            PrecioAdicional = a.PrecioAdicional,
            IdEmpresa = a.IdEmpresa
        }).ToList();
    }
}
