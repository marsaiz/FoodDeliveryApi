using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;
public class CategoriaServicio : ICategoriaServicio
{
    private readonly ICategoriaRepositorio _categoriaRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public CategoriaServicio(ICategoriaRepositorio categoriaRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
        _categoriaRepositorio = categoriaRepositorio;
        _empresaRepositorio = empresaRepositorio;
    }

    public async Task<Categoria> CrearCategoriaAsync(CategoriaCreateDTO categoriaDto)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(categoriaDto.IdEmpresa);
        if (empresa == null)
        {
            throw new KeyNotFoundException("La empresa no existe.");
        }

        var nuevaCategoria = new Categoria
        {
            NombreCategoria = categoriaDto.NombreCategoria,
            IdEmpresa = categoriaDto.IdEmpresa
        };

        return await _categoriaRepositorio.CrearCategoriaAsync(nuevaCategoria);
    }

    public async Task<Categoria> ActualizarCategoriaAsync(int idCategoria, Guid idEmpresa, CategoriaUpdateDTO categoriaDto)
    {
        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(idEmpresa);
        if (empresa == null)
            throw new Exception("La empresa especificada no existe.");

        // 1. Obtener la entidad existente desde el repositorio (y la base de datos)
        var categoriaExistente = await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(idCategoria, idEmpresa);
        if (categoriaExistente == null)
            return null; // O lanzar una excepción si prefieres
        
            //IdAdicional = adicionalDto.IdAdicional,
            categoriaExistente.NombreCategoria = categoriaDto.NombreCategoria;

        return await _categoriaRepositorio.ActualizarCategoriaAsync(categoriaExistente);
    }

    public async Task<bool> EliminarCategoriaAsync(int idCategoria, Guid idEmpresa)
    {
        return await _categoriaRepositorio.EliminarCategoriaAsync(idCategoria, idEmpresa);
    }

    public async Task<Categoria> ObtenerCategoriaPorIdAsync(int idCategoria, Guid idEmpresa)
    {
        return await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(idCategoria, idEmpresa);
    }

    public async Task<List<Categoria>> ObtenerCategoriasPorEmpresaAsync(Guid idEmpresa)
    {
        return await _categoriaRepositorio.ObtenerCategoriasPorEmpresaAsync(idEmpresa);
    }
}
