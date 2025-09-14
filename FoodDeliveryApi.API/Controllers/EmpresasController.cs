using FoodDelivery.Domain.Modelos;
using FoodDelivery.Domain.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetAll()
        {
            var empresas = await _empresaService.GetAllAsync();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetById(Guid id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Empresa empresa)
        {
            await _empresaService.AddAsync(empresa);
            return CreatedAtAction(nameof(GetById), new { id = empresa.IdEmpresa }, empresa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
                return BadRequest();
            await _empresaService.UpdateAsync(empresa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _empresaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
