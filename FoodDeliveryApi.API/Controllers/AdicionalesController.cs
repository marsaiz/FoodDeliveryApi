using FoodDelivery.Domain.Modelos;
using FoodDelivery.Domain.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdicionalesController : ControllerBase
    {
        private readonly IAdicionalService _adicionalService;

        public AdicionalesController(IAdicionalService adicionalService)
        {
            _adicionalService = adicionalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adicional>>> GetAll()
        {
            var adicionales = await _adicionalService.GetAllAsync();
            return Ok(adicionales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adicional>> GetById(int id)
        {
            var adicional = await _adicionalService.GetByIdAsync(id);
            if (adicional == null)
                return NotFound();
            return Ok(adicional);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Adicional adicional)
        {
            await _adicionalService.AddAsync(adicional);
            return CreatedAtAction(nameof(GetById), new { id = adicional.IdAdicional }, adicional);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Adicional adicional)
        {
            if (id != adicional.IdAdicional)
                return BadRequest();
            await _adicionalService.UpdateAsync(adicional);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _adicionalService.DeleteAsync(id);
            return NoContent();
        }
    }
}
