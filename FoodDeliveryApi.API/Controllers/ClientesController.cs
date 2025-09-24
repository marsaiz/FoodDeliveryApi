using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Servicios.DTOs;

namespace FoodDeliveryApi.API.Controllers;

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
        var clientesDTO = clientes.Select(c => new ClienteDTO
        {
            IdCliente = c.IdCliente,
            NombreCliente = c.NombreCliente,
            TelefonoCliente = c.TelefonoCliente,
            EmailCliente = c.EmailCliente,
            Usuario = c.Usuario
        }).ToList();
        return Ok(clientesDTO);
    }

    [HttpGet("{idCliente}")]
    public async Task<ActionResult<Cliente>> GetById(Guid idCliente)
    {
        var cliente = await _clienteService.ObtenerClientePorIdAsync(idCliente);
        if (cliente == null)
            return NotFound();

        var clienteDTO = new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCliente,
            TelefonoCliente = cliente.TelefonoCliente,
            EmailCliente = cliente.EmailCliente,
            Usuario = cliente.Usuario
        };
        return Ok(clienteDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ClienteCreateDTO clienteDTO)
    {
        var cliente = await _clienteService.CrearClienteAsync(clienteDTO);

        var clienteDTOResultado = new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCliente,
            TelefonoCliente = cliente.TelefonoCliente,
            EmailCliente = cliente.EmailCliente,
            Usuario = cliente.Usuario
        };
        return CreatedAtAction(nameof(GetById), new { idCliente = cliente.IdCliente }, clienteDTO);
    }

    [HttpPut("{idCliente}")]
    public async Task<ActionResult> Update(Guid idCliente, ClienteUpdateDTO clienteDTO)
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

    [HttpPut("{idCliente}/cambiar-password")]
    public async Task<ActionResult> ChangePassword(Guid idCliente, ClienteChangePasswordDTO changePasswordDTO)
    {
        var result = await _clienteService.CambiarPasswordAsync(idCliente, changePasswordDTO);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
