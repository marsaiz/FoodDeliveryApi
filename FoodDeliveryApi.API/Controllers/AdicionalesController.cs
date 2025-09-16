using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdicionalesController : ControllerBase
    {
        private readonly IAdicionalServicio _adicionalService;

        public AdicionalesController(IAdicionalServicio adicionalService)
        {
            _adicionalService = adicionalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adicional>>> GetAll(Guid idEmpresa)
        {
            var adicionales = await _adicionalService.ObtenerAdicionalesPorEmpresaAsync(idEmpresa);
            return Ok(adicionales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adicional>> GetById(int adicional, Guid idEmpresa)
        {
            var adicionalSeleccionado = await _adicionalService.ObtenerAdicionalPorIdAsync(adicional, idEmpresa);
            if (adicionalSeleccionado == null)
                return NotFound();
            return Ok(adicionalSeleccionado);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AdicionalDTO adicionalDTO)
        {
            await _adicionalService.CrearAdicionalAsync(adicionalDTO);
            return CreatedAtAction(nameof(GetById), new { id = adicionalDTO.IdAdicional }, adicionalDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AdicionalDTO adicionalDTO)
        {
            if (id != adicionalDTO.IdAdicional)
                return BadRequest();
            await _adicionalService.ActualizarAdicionalAsync(adicionalDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int idAdicional, Guid idEmpresa)
        {
            var adicionalExistente = await _adicionalService.ObtenerAdicionalPorIdAsync(idAdicional, idEmpresa);
            if (adicionalExistente == null)
            {
                return NotFound();
            }
            await _adicionalService.EliminarAdicionalAsync(idAdicional, idEmpresa);
            return NoContent();
        }
    }
}
