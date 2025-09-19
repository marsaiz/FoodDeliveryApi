using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;
public class CategoriaServicio : ICategoriaServicio
{
    private readonly ICategoriaRepositorio _categoriaRepositorio;

    public CategoriaServicio(ICategoriaRepositorio categoriaRepositorio)
    {
        _categoriaRepositorio = categoriaRepositorio;
    }

    public async Task<Categoria> CrearCategoriaAsync(CategoriaDTO categoriaDto)
    {
        var categoria = new Categoria
        {
            NombreCategoria = categoriaDto.NombreCategoria,
            IdEmpresa = categoriaDto.IdEmpresa
        };

        return await _categoriaRepositorio.CrearCategoriaAsync(categoria);
    }

    public async Task<Categoria> ActualizarCategoriaAsync(CategoriaDTO categoriaDto)
    {
        if (categoriaDto.IdCategoria == null)
        {
            throw new ArgumentException("El IdCategoria no puede ser nulo para la actualización.");
        }

        var categoriaExistente = await _categoriaRepositorio.
                            ObtenerCategoriaPorIdAsync(categoriaDto.IdCategoria.Value, categoriaDto.IdEmpresa);
        if (categoriaExistente == null)
        {
            throw new KeyNotFoundException("La categoría no existe.");
        }

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
