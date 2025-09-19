using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

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

        [HttpGet("{idAdicional}/{idEmpresa}")]
        public async Task<ActionResult<Adicional>> GetById(int idAdicional, Guid idEmpresa)
        {
            var adicionalSeleccionado = await _adicionalService.ObtenerAdicionalPorIdAsync(idAdicional, idEmpresa);
            if (adicionalSeleccionado == null)
                return NotFound();
            return Ok(adicionalSeleccionado);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AdicionalCreateDTO adicionalDTO)
        {
            var creado = await _adicionalService.CrearAdicionalAsync(adicionalDTO);

            // Mapeo manual, a AdicionalDT. Hacía referencia circularn en el swagger.
            //  En el servicio se recibe el DTO de AdicionalCreateDTO
            var dto = new AdicionalDTO
            {
                IdAdicional = creado.IdAdicional,
                NombreAdicional = creado.NombreAdicional,
                PrecioAdicional = creado.PrecioAdicional,
                IdEmpresa = creado.IdEmpresa
            };
            // Suponiendo que 'creado' es el objeto Adicional creado y tiene IdAdicional y IdEmpresa
            return CreatedAtAction(nameof(GetById), new { idAdicional = dto.IdAdicional, idEmpresa = dto.IdEmpresa }, dto);
        }

        [HttpPut("{idAdicional}")]
        public async Task<ActionResult> Update(int idAdicional, AdicionalUpdateDTO adicionalDTO)
        {
            if (idAdicional != adicionalDTO.IdAdicional)
                return BadRequest();
            await _adicionalService.ActualizarAdicionalAsync(adicionalDTO);
            return NoContent();
        }

        [HttpDelete("{idAdicional}/{idEmpresa}")]
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
