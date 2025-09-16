using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Domain.Modelos;

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
    
        var nuevoAdicional = new Adicional
        {
            NombreAdicional = adicionalDto.NombreAdicional,
            PrecioAdicional = adicionalDto.PrecioAdicional ?? 0, // Asignar 0 si es null
            IdEmpresa = adicionalDto.IdEmpresa
        };
        return await _adicionalRepositorio.CrearAdicionalAsync(nuevoAdicional);
    }

    public async Task<Adicional> ActualizarAdicionalAsync(AdicionalUpdateDTO adicionalDto)
    {
        if (adicionalDto.IdAdicional == null)
        {
            throw new ArgumentException("IdAdicional es requerido para actualizar.");
        }

        // 1. Obtener la entidad existente desde el repositorio (y la base de datos)
        var adicionalExistente = await _adicionalRepositorio.ObtenerAdicionalPorIdAsync(adicionalDto.IdAdicional, adicionalDto.IdEmpresa);

        if (adicionalExistente == null)
        {
            // Manejar el caso donde no se encuentra la entidad
            throw new KeyNotFoundException($"Adicional con Id {adicionalDto.IdAdicional} no encontrado.");
        }

        // 2. Actualizar las propiedades de la entidad existente
        adicionalExistente.NombreAdicional = adicionalDto.NombreAdicional;
        adicionalExistente.PrecioAdicional = adicionalDto.PrecioAdicional ?? 0;
        // No actualices IdEmpresa, ya que esta clave generalmente no cambia

        // 3. Persistir los cambios usando el repositorio
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
