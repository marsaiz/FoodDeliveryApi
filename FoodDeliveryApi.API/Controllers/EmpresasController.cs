using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

namespace FoodDeliveryApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresaServicio _empresaService;

    public EmpresasController(IEmpresaServicio empresaService)
    {
        _empresaService = empresaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empresa>>> GetAll()
    {
        var empresas = await _empresaService.ObtenerEmpresasAsync();
        // Mapear las entidades a DTOs antes de devolver
        var empresasDTO = empresas.Select(e => new EmpresaDTO
        {
            IdEmpresa = e.IdEmpresa,
            Nombre = e.Nombre,
            Telefono = e.Telefono,
            Email = e.Email,
            Direccion = e.Direccion,
            Latitud = e.Latitud,
            Longitud = e.Longitud,
            EstaAbierta = e.EstaAbierta,
            Usuario = e.Usuario
        }).ToList();
        return Ok(empresasDTO);
    }

    [HttpGet("{idEmpresa}")]
    public async Task<ActionResult<EmpresaDTO>> GetById(Guid idEmpresa)
    {
        var empresa = await _empresaService.ObtenerEmpresaPorIdAsync(idEmpresa);
        if (empresa == null)
            return NotFound();

        var empresaDTO = new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Direccion = empresa.Direccion,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            EstaAbierta = empresa.EstaAbierta,
            Usuario = empresa.Usuario
        };
        return Ok(empresaDTO);
    }

    [HttpPost]
    public async Task<ActionResult<EmpresaDTO>> Create(EmpresaCreateDTO empresaDTO)
    {
        var empresa = await _empresaService.CrearEmpresaAsync(empresaDTO);

        var empresaDTOResultado = new EmpresaDTO
        {
            IdEmpresa = empresa.IdEmpresa,
            Nombre = empresa.Nombre,
            Telefono = empresa.Telefono,
            Email = empresa.Email,
            Direccion = empresa.Direccion,
            Latitud = empresa.Latitud,
            Longitud = empresa.Longitud,
            EstaAbierta = empresa.EstaAbierta,
            Usuario = empresa.Usuario
        };
        return CreatedAtAction(nameof(GetById), new { idEmpresa = empresa.IdEmpresa }, empresaDTO);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] EmpresaLoginDTO dto)
    {
        var empresa = await _empresaService.LoginAsync(dto);
        if (empresa == null)
            return Unauthorized();
        return Ok(new { idEmpresa = empresa.IdEmpresa, nombre = empresa.Nombre });
    }

    [HttpPut("{idEmpresa}")]
    public async Task<ActionResult> Update(Guid idEmpresa, EmpresaUpdateDTO empresaDTO)
    {
        if (idEmpresa != empresaDTO.IdEmpresa)
            return BadRequest();
        await _empresaService.ActualizarEmpresaAsync(empresaDTO);
        return NoContent();
    }

    [HttpDelete("{idEmpresa}")]
    public async Task<ActionResult> Delete(Guid idEmpresa)
    {
        await _empresaService.EliminarEmpresaAsync(idEmpresa);
        return NoContent();
    }

    [HttpPut("{idEmpresa}/cambiar-password")]
    public async Task<ActionResult> CambiarPassword(Guid idEmpresa, EmpresaChangePasswordDTO dto)
    {
        var resultado = await _empresaService.CambiarPasswordAsync(idEmpresa, dto);
        if (!resultado)
            return BadRequest("La contraseña actual es incorrecta.");

        return NoContent();
    }
}
