using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Interfaces;

public interface ICategoriaServicio
{
    Task<Categoria> CrearCategoriaAsync(CategoriaCreateDTO categoria);
    Task<Categoria> ActualizarCategoriaAsync(CategoriaUpdateDTO categoria);
    Task<bool> EliminarCategoriaAsync(int idCategoria, Guid idEmpresa);
    Task<Categoria> ObtenerCategoriaPorIdAsync(int idCategoria, Guid idEmpresa);
    Task<List<Categoria>> ObtenerCategoriasPorEmpresaAsync(Guid idEmpresa);
}
