using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

namespace FoodDeliveryApi.API.Controllers;

[ApiController]
[Route("api/[controller]")] // la palabra controller se reemplaza por el nombre del controlador sin el sufijo "Controller"
public class ProductoController : ControllerBase
{
    private readonly IProductoServicio _productoService;

    public ProductoController(IProductoServicio productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetAll(Guid idEmpresa)
    {
        var productos = await _productoService.ObtenerProductosPorEmpresaAsync(idEmpresa);
        return Ok(productos);
    }

    [HttpGet("{idProducto}/{idEmpresa}")]
    public async Task<ActionResult<Producto>> GetById(int idProducto, Guid idEmpresa)
    {
        var productoSeleccionado = await _productoService.ObtenerProductoPorIdAsync(idProducto, idEmpresa);
        if (productoSeleccionado == null)
            return NotFound();
        return Ok(productoSeleccionado);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductoCreateDTO productoDTO)
    {
        var creado = await _productoService.CrearProductoAsync(productoDTO);

        // Mapeo manual, a ProductoDTO. Hacía referencia circularn en el swagger.
        //  En el servicio se recibe el DTO de ProductoCreateDTO
        var dto = new ProductoDTO
        {
            IdProducto = creado.IdProducto,
            NombreProducto = creado.NombreProducto,
            DescripcionProducto = creado.DescripcionProducto,
            PrecioProducto = creado.PrecioProducto,
            IdCategoria = creado.IdCategoria,
            IdEmpresa = creado.IdEmpresa
        };
        // Suponiendo que 'creado' es el objeto Producto creado y tiene IdProducto y IdEmpresa
        return CreatedAtAction(nameof(GetById), new { idProducto = dto.IdProducto, idEmpresa = dto.IdEmpresa }, dto);
    }

    [HttpPut("{idProducto}/{idEmpresa}")]
    public async Task<ActionResult> Update(int idProducto, Guid idEmpresa, ProductoUpdateDTO productoDTO)
    {
        var productoUpdate = new ProductoUpdateDTO
        {
            NombreProducto = productoDTO.NombreProducto,
            DescripcionProducto = productoDTO.DescripcionProducto,
            PrecioProducto = productoDTO.PrecioProducto,
            ImagenUrl = productoDTO.ImagenUrl
        };
        var actualizado = await _productoService.ActualizarProductoAsync(idProducto, idEmpresa, productoUpdate);
        if (actualizado == null)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{idProducto}/{idEmpresa}")]
    public async Task<ActionResult> Delete(int idProducto, Guid idEmpresa)
    {
        await _productoService.EliminarProductoAsync(idProducto, idEmpresa);
        return NoContent();
    }
}
