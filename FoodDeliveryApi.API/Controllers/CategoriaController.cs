using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaServicio _categoriaServicio;

    public CategoriaController(ICategoriaServicio categoriaServicio)
    {
        _categoriaServicio = categoriaServicio;
    }

    [HttpGet("empresa/{idEmpresa}")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAll(Guid idEmpresa)
    {
        var categorias = await _categoriaServicio.ObtenerCategoriasPorEmpresaAsync(idEmpresa);
        return Ok(categorias);
    }

    [HttpGet("{idCategoria}/{idEmpresa}")]
    public async Task<ActionResult<Categoria>> GetById(int idCategoria, Guid idEmpresa)
    {
        var categoriaSeleccionada = await _categoriaServicio.ObtenerCategoriaPorIdAsync(idCategoria, idEmpresa);
        if (categoriaSeleccionada == null)
            return NotFound();
        return Ok(categoriaSeleccionada);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CategoriaCreateDTO categoriaDTO)
    {
        var creado = await _categoriaServicio.CrearCategoriaAsync(categoriaDTO);

        var dto = new CategoriaDTO
        {
            IdCategoria = creado.IdCategoria,
            NombreCategoria = creado.NombreCategoria,
            IdEmpresa = creado.IdEmpresa
        };
        // Suponiendo que 'creado' es el objeto Categoria creado y tiene IdCategoria y IdEmpresa
        return CreatedAtAction(nameof(GetById), new { idCategoria = creado.IdCategoria, idEmpresa = creado.IdEmpresa }, dto);
    }

    [HttpPut("{idCategoria}/{idEmpresa}")]
    public async Task<ActionResult> Update(int idCategoria, Guid idEmpresa, [FromBody] CategoriaUpdateDTO categoriaDTO)
    {
        var categoriaUpdate = new CategoriaUpdateDTO
        {
            NombreCategoria = categoriaDTO.NombreCategoria
        };

        var actualizado = await _categoriaServicio.ActualizarCategoriaAsync(idCategoria, idEmpresa, categoriaDTO);
        if (actualizado == null)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{idCategoria}/{idEmpresa}")]
    public async Task<ActionResult> Delete(int idCategoria, Guid idEmpresa)
    {
        var categoriaExistente = await _categoriaServicio.ObtenerCategoriaPorIdAsync(idCategoria, idEmpresa);
        if (categoriaExistente == null)
            return NotFound();

        await _categoriaServicio.EliminarCategoriaAsync(idCategoria, idEmpresa);
        return NoContent();
    }
}
