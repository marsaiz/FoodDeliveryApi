using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class DireccionClienteServicio : IDireccionClienteServicio
{
    private readonly IDireccionClienteRepositorio _direccionClienteRepositorio;
    private readonly IClienteRepositorio _clienteRepositorio;

    public DireccionClienteServicio(IDireccionClienteRepositorio direccionClienteRepositorio, IClienteRepositorio clienteRepositorio)
    {
        _direccionClienteRepositorio = direccionClienteRepositorio;
        _clienteRepositorio = clienteRepositorio;
    }

    public async Task<DireccionCliente> CrearDireccionClienteAsync(DireccionClienteDTO direccionDto)
    {
        var clienteExistente = await _clienteRepositorio.ObtenerClientePorIdAsync(direccionDto.IdCliente);
        if (clienteExistente == null)
            throw new KeyNotFoundException("El cliente especificado no existe.");

        var direccion = new DireccionCliente
        {
            Calle = direccionDto.Calle,
            Numero = direccionDto.Numero,
            PisoDepto = direccionDto.PisoDepto,
            Ciudad = direccionDto.Ciudad,
            CodigoPostal = direccionDto.CodigoPostal,
            Referencia = direccionDto.Referencia,
            Latitud = direccionDto.Latitud,
            Longitud = direccionDto.Longitud,
            IdCliente = direccionDto.IdCliente
        };

        return await _direccionClienteRepositorio.CrearDireccionClienteAsync(direccion);
    }

    public async Task<DireccionCliente> ActualizarDireccionClienteAsync(DireccionClienteDTO direccionDto)
    {
        if (direccionDto.IdDireccionCliente == null)
        {
            throw new ArgumentException("El IdDireccionCliente no puede ser nulo para actualizar una dirección.");
        }

        var direccionExistente = await _direccionClienteRepositorio.ObtenerDireccionClientePorIdAsync(direccionDto.IdDireccionCliente.Value, Guid.Empty);
        if (direccionExistente == null)
        {
            throw new KeyNotFoundException("No se encontró la dirección del cliente.");
        }

        // Actualizar los campos de la dirección existente
        direccionExistente.Calle = direccionDto.Calle;
        direccionExistente.Numero = direccionDto.Numero;
        direccionExistente.PisoDepto = direccionDto.PisoDepto;
        direccionExistente.Ciudad = direccionDto.Ciudad;
        direccionExistente.CodigoPostal = direccionDto.CodigoPostal;
        direccionExistente.Referencia = direccionDto.Referencia;
        direccionExistente.Latitud = direccionDto.Latitud;
        direccionExistente.Longitud = direccionDto.Longitud;

        return await _direccionClienteRepositorio.ActualizarDireccionClienteAsync(direccionExistente);
    }

    public async Task<bool> EliminarDireccionClienteAsync(int idDireccion, Guid idCliente)
    {
        return await _direccionClienteRepositorio.EliminarDireccionClienteAsync(idDireccion, idCliente);
    }

    public async Task<DireccionCliente> ObtenerDireccionClientePorIdAsync(int idDireccion, Guid idCliente)
    {
        return await _direccionClienteRepositorio.ObtenerDireccionClientePorIdAsync(idDireccion, idCliente);
    }

    public async Task<List<DireccionCliente>> ObtenerDireccionesPorClienteAsync(Guid idCliente)
    {
        return await _direccionClienteRepositorio.ObtenerDireccionesPorClienteAsync(idCliente);
    }
}
