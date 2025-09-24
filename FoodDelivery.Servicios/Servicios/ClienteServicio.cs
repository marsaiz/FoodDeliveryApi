using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using static FoodDelivery.Servicios.Utils.PasswordHelper;

namespace FoodDelivery.Servicios.Servicios;

public class ClienteServicio : IClienteServicio
{
    private readonly IClienteRepositorio _clienteRepositorio;

    public ClienteServicio(IClienteRepositorio clienteRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
    }

    public async Task<Cliente> CrearClienteAsync(ClienteCreateDTO clienteDTO)
    {
        // Verificar si el usuario o email ya existen podría ser una buena práctica aquí.
        var clienteExistentePorUsuario = await _clienteRepositorio.ObtenerClientePorUsuarioAsync(clienteDTO.EmailCliente);
        if (clienteExistentePorUsuario != null)
        {
            throw new ArgumentException("Ya existe un cliente registrado con este email.");
        }
        var clienteExistentePorEmail = await _clienteRepositorio.ObtenerClientePorEmailAsync(clienteDTO.EmailCliente);
        if (clienteExistentePorEmail != null)
        {
            throw new ArgumentException("Ya existe un cliente registrado con este email.");
        }
        string salt = GenerateSalt();
        string passwordHash = HashPassword(clienteDTO.Password, salt);

        var nuevoCliente = new Cliente
        {
            IdCliente = Guid.NewGuid(),
            NombreCliente = clienteDTO.NombreCliente,
            EmailCliente = clienteDTO.EmailCliente,
            TelefonoCliente = clienteDTO.TelefonoCliente,
            Usuario = clienteDTO.Usuario,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
        };
        await _clienteRepositorio.CrearClienteAsync(nuevoCliente);
        return nuevoCliente;
    }

    public async Task<Cliente> ActualizarClienteAsync(ClienteUpdateDTO clienteDTO)
    {
        if (clienteDTO.IdCliente == null || clienteDTO.IdCliente == Guid.Empty)
        {
            throw new ArgumentException("El IdCliente es obligatorio para actualizar un cliente.");
        }

        var clienteExistente = await _clienteRepositorio.ObtenerClientePorIdAsync(clienteDTO.IdCliente);
        if (clienteExistente == null) return null;

        clienteExistente.NombreCliente = clienteDTO.NombreCliente;
        clienteExistente.EmailCliente = clienteDTO.EmailCliente;
        clienteExistente.TelefonoCliente = clienteDTO.TelefonoCliente;

        return await _clienteRepositorio.ActualizarClienteAsync(clienteExistente);
    }

    public async Task<bool> EliminarClienteAsync(Guid idCliente)
    {
        return await _clienteRepositorio.EliminarClienteAsync(idCliente);
    }

    public async Task<ClienteDTO> ObtenerClientePorIdAsync(Guid idCliente)
    {
        var cliente = await _clienteRepositorio.ObtenerClientePorIdAsync(idCliente);
        if (cliente == null) return null;
        return new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCliente,
            EmailCliente = cliente.EmailCliente,
            TelefonoCliente = cliente.TelefonoCliente,
            Usuario = cliente.Usuario
        };
    }

    public async Task<List<ClienteDTO>> ObtenerClientesAsync()
    {
        var clientes = await _clienteRepositorio.ObtenerClientesAsync();
        return clientes.Select(c => new ClienteDTO
        {
            IdCliente = c.IdCliente,
            NombreCliente = c.NombreCliente,
            EmailCliente = c.EmailCliente,
            TelefonoCliente = c.TelefonoCliente,
            Usuario = c.Usuario
        }).ToList();
    }

    public async Task<ClienteDTO> ObtenerClientePorUsuarioAsync(string usuario)
    {
        var cliente = await _clienteRepositorio.ObtenerClientePorUsuarioAsync(usuario);
        if (cliente == null) return null;
        return new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCliente,
            EmailCliente = cliente.EmailCliente,
            TelefonoCliente = cliente.TelefonoCliente,
            Usuario = cliente.Usuario
        };
    }

    public async Task<ClienteDTO> ObtenerClientePorEmailAsync(string email)
    {
        var cliente = await _clienteRepositorio.ObtenerClientePorEmailAsync(email);
        if (cliente == null) return null;
        return new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCliente,
            EmailCliente = cliente.EmailCliente,
            TelefonoCliente = cliente.TelefonoCliente,
            Usuario = cliente.Usuario
        };
    }
    public async Task<bool> CambiarPasswordAsync(Guid idCliente, ClienteChangePasswordDTO dto)
    {
        var clienteExistente = await _clienteRepositorio.ObtenerClientePorIdAsync(idCliente);
        if (clienteExistente == null) return false;

        if (clienteExistente.PasswordHash != HashPassword(dto.PasswordActual, clienteExistente.PasswordSalt))
        {
            throw new ArgumentException("La contraseña actual es incorrecta.");
        }

        string nuevoSalt = GenerateSalt();
        string nuevoPasswordHash = HashPassword(dto.NuevoPassword, nuevoSalt);

        clienteExistente.PasswordSalt = nuevoSalt;
        clienteExistente.PasswordHash = nuevoPasswordHash;

        await _clienteRepositorio.ActualizarClienteAsync(clienteExistente);
        return true;
    }
}
