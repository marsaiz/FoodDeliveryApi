using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface ICategoriaServicio
{
    Task<CategoriaDTO> CrearCategoriaAsync(CategoriaCreateDTO categoria);
    Task<CategoriaDTO> ActualizarCategoriaAsync(int idCategoria, Guid idEmpresa, CategoriaUpdateDTO categoriaUpdateDTO);
    Task<bool> EliminarCategoriaAsync(int idCategoria, Guid idEmpresa);
    Task<CategoriaDTO> ObtenerCategoriaPorIdAsync(int idCategoria, Guid idEmpresa);
    Task<List<CategoriaDTO>> ObtenerCategoriasPorEmpresaAsync(Guid idEmpresa);
}