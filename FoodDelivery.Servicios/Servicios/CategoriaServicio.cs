using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;

namespace FoodDelivery.Servicios.Servicios;
public class CategoriaServicio : ICategoriaServicio
{
    private readonly ICategoriaRepositorio _categoriaRepositorio;
    private readonly IEmpresaServicio _empresaServicio;

    public CategoriaServicio(ICategoriaRepositorio categoriaRepositorio, IEmpresaServicio empresaServicio)
    {
        _categoriaRepositorio = categoriaRepositorio;
        _empresaServicio = empresaServicio;
    }

    public async Task<CategoriaDTO> CrearCategoriaAsync(CategoriaCreateDTO categoriaDto)
    {
        var empresa = await _empresaServicio.ObtenerEmpresaPorIdAsync(categoriaDto.IdEmpresa);
        if (empresa == null)
        {
            throw new KeyNotFoundException("La empresa no existe.");
        }

        var nuevaCategoria = new Categoria
        {
            NombreCategoria = categoriaDto.NombreCategoria,
            IdEmpresa = categoriaDto.IdEmpresa
        };

        var categoriaCreada = await _categoriaRepositorio.CrearCategoriaAsync(nuevaCategoria);
        return new CategoriaDTO
        {
            IdCategoria = categoriaCreada.IdCategoria,
            NombreCategoria = categoriaCreada.NombreCategoria,
            IdEmpresa = categoriaCreada.IdEmpresa
        };
    }

    public async Task<CategoriaDTO> ActualizarCategoriaAsync(int idCategoria, Guid idEmpresa, CategoriaUpdateDTO categoriaDto)
    {
        var empresa = await _empresaServicio.ObtenerEmpresaPorIdAsync(idEmpresa);
        if (empresa == null)
            throw new Exception("La empresa especificada no existe.");

        // 1. Obtener la entidad existente desde el repositorio (y la base de datos)
        var categoriaExistente = await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(idCategoria, idEmpresa);
        if (categoriaExistente == null)
            return null; // O lanzar una excepción si prefieres
        
        // 2. Actualizar los campos necesarios
            categoriaExistente.NombreCategoria = categoriaDto.NombreCategoria;

        var categoriaActualizada = await _categoriaRepositorio.ActualizarCategoriaAsync(categoriaExistente);
        return new CategoriaDTO
        {
            IdCategoria = categoriaActualizada.IdCategoria,
            NombreCategoria = categoriaActualizada.NombreCategoria,
            IdEmpresa = categoriaActualizada.IdEmpresa
        };
    }

    public async Task<bool> EliminarCategoriaAsync(int idCategoria, Guid idEmpresa)
    {
        return await _categoriaRepositorio.EliminarCategoriaAsync(idCategoria, idEmpresa);
    }

    public async Task<CategoriaDTO> ObtenerCategoriaPorIdAsync(int idCategoria, Guid idEmpresa)
    {
        var categoria = await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(idCategoria, idEmpresa);
        if (categoria == null)
            return null;

        return new CategoriaDTO
        {
            IdCategoria = categoria.IdCategoria,
            NombreCategoria = categoria.NombreCategoria,
            IdEmpresa = categoria.IdEmpresa
        };
    }

    public async Task<List<CategoriaDTO>> ObtenerCategoriasPorEmpresaAsync(Guid idEmpresa)
    {
        var categorias = await _categoriaRepositorio.ObtenerCategoriasPorEmpresaAsync(idEmpresa);
        return categorias.Select(c => new CategoriaDTO
        {
            IdCategoria = c.IdCategoria,
            NombreCategoria = c.NombreCategoria,
            IdEmpresa = c.IdEmpresa
        }).ToList();
    }
}
