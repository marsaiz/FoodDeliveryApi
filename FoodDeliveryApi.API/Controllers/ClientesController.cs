using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServicio _clienteService;

        public ClientesController(IClienteServicio clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll()
        {
            var clientes = await _clienteService.ObtenerClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{idCliente}")]
        public async Task<ActionResult<Cliente>> GetById(Guid idCliente)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(idCliente);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClienteDTO clienteDTO)
        {
            await _clienteService.CrearClienteAsync(clienteDTO);
            return CreatedAtAction(nameof(GetById), new { idCliente = clienteDTO.IdCliente }, clienteDTO);
        }

        [HttpPut("{idCliente}")]
        public async Task<ActionResult> Update(Guid idCliente, ClienteDTO clienteDTO)
        {
            if (idCliente != clienteDTO.IdCliente)
                return BadRequest();
            await _clienteService.ActualizarClienteAsync(clienteDTO);
            return NoContent();
        }

        [HttpDelete("{idCliente}")]
        public async Task<ActionResult> Delete(Guid idCliente)
        {
            await _clienteService.EliminarClienteAsync(idCliente);
            return NoContent();
        }
    }
}
