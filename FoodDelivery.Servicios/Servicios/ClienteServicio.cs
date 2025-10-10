using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;
using static FoodDelivery.Servicios.Utils.PasswordHelper;

namespace FoodDelivery.Servicios.Servicios;

public class ClienteServicio : IClienteServicio
{
    private readonly IClienteRepositorio _clienteRepositorio;

    public ClienteServicio(IClienteRepositorio clienteRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
    }

    public async Task<ClienteDTO> CrearClienteAsync(ClienteCreateDTO clienteDTO)
    {
        // Validaciones básicas
        if (string.IsNullOrWhiteSpace(clienteDTO.Usuario))
        {
            throw new ArgumentException("El usuario es obligatorio.");
        }
        if (string.IsNullOrWhiteSpace(clienteDTO.Password) || clienteDTO.Password.Length < 6)
        {
            throw new ArgumentException("La contraseña es obligatoria y debe tener al menos 6 caracteres.");
        }
        if (string.IsNullOrWhiteSpace(clienteDTO.EmailCliente))
        {
            throw new ArgumentException("El email es obligatorio.");
        }

        // Verificar si ya existe un cliente con el mismo usuario o email
        var clienteExistentePorUsuario = await _clienteRepositorio.ObtenerClientePorUsuarioAsync(clienteDTO.Usuario);
        if (clienteExistentePorUsuario != null)
        {
            throw new ArgumentException("Ya existe un cliente registrado con este usuario.");
        }
        var clienteExistentePorEmail = await _clienteRepositorio.ObtenerClientePorEmailAsync(clienteDTO.EmailCliente);
        if (clienteExistentePorEmail != null)
        {
            throw new ArgumentException("Ya existe un cliente registrado con este email.");
        }

        // Crear el cliente con hash de la contraseña
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

        return new ClienteDTO
        {
            IdCliente = nuevoCliente.IdCliente,
            NombreCliente = nuevoCliente.NombreCliente,
            EmailCliente = nuevoCliente.EmailCliente,
            TelefonoCliente = nuevoCliente.TelefonoCliente,
            Usuario = nuevoCliente.Usuario
        };
    }

    public async Task<ClienteDTO?> ActualizarClienteAsync(ClienteUpdateDTO clienteDTO)
    {
        if (clienteDTO?.IdCliente == null || clienteDTO.IdCliente == Guid.Empty)
        {
            throw new ArgumentException("El IdCliente es obligatorio para actualizar un cliente.");
        }

        var clienteExistente = await _clienteRepositorio.ObtenerClientePorIdAsync(clienteDTO.IdCliente);
        if (clienteExistente == null) return null;

        clienteExistente.NombreCliente = clienteDTO?.NombreCliente ?? clienteExistente.NombreCliente;
        clienteExistente.EmailCliente = clienteDTO?.EmailCliente ?? clienteExistente.EmailCliente;
        clienteExistente.TelefonoCliente = clienteDTO?.TelefonoCliente ?? clienteExistente.TelefonoCliente;

        var clienteActualizado = await _clienteRepositorio.ActualizarClienteAsync(clienteExistente);

        // Mapear a DTO y devolver
        return new ClienteDTO
        {
            IdCliente = clienteActualizado.IdCliente,
            NombreCliente = clienteActualizado.NombreCliente,
            EmailCliente = clienteActualizado.EmailCliente,
            TelefonoCliente = clienteActualizado.TelefonoCliente,
            Usuario = clienteActualizado.Usuario
        };
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

    public async Task<ClienteDTO?> ObtenerClientePorEmailAsync(string email)
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

    public async Task<ClienteDTO?> LoginAsync(ClienteLoginDTO loginDTO)
    {
        var cliente = await _clienteRepositorio.ObtenerClientePorUsuarioAsync(loginDTO.Usuario);
        if (cliente == null)
            return null;
        if (!VerifyPassword(loginDTO.Password, cliente.PasswordHash ?? string.Empty, cliente.PasswordSalt ?? string.Empty))
            return null;
        return new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            NombreCliente = cliente.NombreCliente,
            TelefonoCliente = cliente.TelefonoCliente,
            EmailCliente = cliente.EmailCliente,
            Usuario = cliente.Usuario
        };
    }
}
