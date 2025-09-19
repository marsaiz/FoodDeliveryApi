using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class ClienteServicio : IClienteServicio
{
    private readonly IClienteRepositorio _clienteRepositorio;

    public ClienteServicio(IClienteRepositorio clienteRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
    }

    public async Task<Cliente> CrearClienteAsync(ClienteDTO clienteDTO)
    {
        var nuevoCliente = new Cliente
        {
            IdCliente = Guid.NewGuid(),
            Nombre = clienteDTO.Nombre,
            Email = clienteDTO.Email,
            Telefono = clienteDTO.Telefono,
        };
        return await _clienteRepositorio.CrearClienteAsync(nuevoCliente);
    }

    public async Task<Cliente> ActualizarClienteAsync(ClienteDTO clienteDTO)
    {
        if (clienteDTO.IdCliente == null || clienteDTO.IdCliente == Guid.Empty)
        {
            throw new ArgumentException("El IdCliente es obligatorio para actualizar un cliente.");
        }

        var clienteExistente = await _clienteRepositorio.ObtenerClientePorIdAsync(clienteDTO.IdCliente.Value);
        if (clienteExistente == null) return null;

        clienteExistente.Nombre = clienteDTO.Nombre;
        clienteExistente.Email = clienteDTO.Email;
        clienteExistente.Telefono = clienteDTO.Telefono;

        return await _clienteRepositorio.ActualizarClienteAsync(clienteExistente);
    }

    public async Task<bool> EliminarClienteAsync(Guid idCliente)
    {
        return await _clienteRepositorio.EliminarClienteAsync(idCliente);
    }

    public async Task<Cliente> ObtenerClientePorIdAsync(Guid idCliente)
    {
        return await _clienteRepositorio.ObtenerClientePorIdAsync(idCliente);
    }

    public async Task<List<Cliente>> ObtenerClientesAsync()
    {
        return await _clienteRepositorio.ObtenerClientesAsync();
    }
}
