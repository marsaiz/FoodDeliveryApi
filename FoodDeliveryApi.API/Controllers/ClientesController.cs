using FoodDelivery.Domain.Modelos;
using FoodDelivery.Domain.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(Guid id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
                return BadRequest();
            await _clienteService.UpdateAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
