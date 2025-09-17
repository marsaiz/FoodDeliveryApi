using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.API.Controllers
{
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
            return Ok(empresas);
        }

        [HttpGet("{idEmpresa}")]
        public async Task<ActionResult<Empresa>> GetById(Guid idEmpresa)
        {
            var empresa = await _empresaService.ObtenerEmpresaPorIdAsync(idEmpresa);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmpresaDTO empresaDTO)
        {
            var empresa = await _empresaService.CrearEmpresaAsync(empresaDTO);
            return CreatedAtAction(nameof(GetById), new { idEmpresa = empresaDTO.IdEmpresa }, empresaDTO);
        }

        [HttpPut("{idEmpresa}")]
        public async Task<ActionResult> Update(Guid idEmpresa, EmpresaDTO empresaDTO)
        {
            if (idEmpresa != empresaDTO.IdEmpresa)
                return BadRequest();
            await _empresaService.ObtenerEmpresaPorIdAsync(empresaDTO.IdEmpresa.Value);
            return NoContent();
        }

        [HttpDelete("{idEmpresa}")]
        public async Task<ActionResult> Delete(Guid idEmpresa)
        {
            await _empresaService.EliminarEmpresaAsync(idEmpresa);
            return NoContent();
        }
    }
}
