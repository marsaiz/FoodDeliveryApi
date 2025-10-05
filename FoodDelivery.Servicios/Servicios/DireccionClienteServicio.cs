using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;

namespace FoodDelivery.Servicios.Servicios;

public class DireccionClienteServicio : IDireccionClienteServicio
{
    private readonly IDireccionClienteRepositorio _direccionClienteRepositorio;
    private readonly IClienteServicio _clienteServicio;

    public DireccionClienteServicio(IDireccionClienteRepositorio direccionClienteRepositorio, IClienteServicio clienteServicio)
    {
        _direccionClienteRepositorio = direccionClienteRepositorio;
        _clienteServicio = clienteServicio;
    }

    public async Task<DireccionClienteDTO> CrearDireccionClienteAsync(DireccionClienteCreateDTO direccionDto)
    {
        var clienteExistente = await _clienteServicio.ObtenerClientePorIdAsync(direccionDto.IdCliente);
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

        var direccionCreada = await _direccionClienteRepositorio.CrearDireccionClienteAsync(direccion);
        return new DireccionClienteDTO
        {
            IdDireccionCliente = direccionCreada.IdDireccionCliente,
            Calle = direccionCreada.Calle,
            Numero = direccionCreada.Numero,
            PisoDepto = direccionCreada.PisoDepto,
            Ciudad = direccionCreada.Ciudad,
            CodigoPostal = direccionCreada.CodigoPostal,
            Referencia = direccionCreada.Referencia,
            Latitud = direccionCreada.Latitud,
            Longitud = direccionCreada.Longitud,
            IdCliente = direccionCreada.IdCliente
        };
    }

    public async Task<DireccionClienteDTO> ActualizarDireccionClienteAsync(int idDireccionCliente, Guid idCliente, DireccionClienteUpdateDTO direccionDto)
    {
        var direccionExistente = await _direccionClienteRepositorio.ObtenerDireccionClientePorIdAsync(idDireccionCliente, idCliente);
        if (direccionExistente == null) return null;
            
        // Actualizar los campos de la dirección existente
        direccionExistente.Calle = direccionDto.Calle;
        direccionExistente.Numero = direccionDto.Numero;
        direccionExistente.PisoDepto = direccionDto.PisoDepto;
        direccionExistente.Ciudad = direccionDto.Ciudad;
        direccionExistente.CodigoPostal = direccionDto.CodigoPostal;
        direccionExistente.Referencia = direccionDto.Referencia;
        direccionExistente.Latitud = direccionDto.Latitud;
        direccionExistente.Longitud = direccionDto.Longitud;

        var direccionActualizada = await _direccionClienteRepositorio.ActualizarDireccionClienteAsync(direccionExistente);
        return new DireccionClienteDTO
        {
            IdDireccionCliente = direccionActualizada.IdDireccionCliente,
            Calle = direccionActualizada.Calle,
            Numero = direccionActualizada.Numero,
            PisoDepto = direccionActualizada.PisoDepto,
            Ciudad = direccionActualizada.Ciudad,
            CodigoPostal = direccionActualizada.CodigoPostal,
            Referencia = direccionActualizada.Referencia,
            Latitud = direccionActualizada.Latitud,
            Longitud = direccionActualizada.Longitud,
            IdCliente = direccionActualizada.IdCliente
        };
    }

    public async Task<bool> EliminarDireccionClienteAsync(int idDireccion, Guid idCliente)
    {
        return await _direccionClienteRepositorio.EliminarDireccionClienteAsync(idDireccion, idCliente);
    }

    public async Task<DireccionClienteDTO> ObtenerDireccionClientePorIdAsync(int idDireccion, Guid idCliente)
    {
        var direccion = await _direccionClienteRepositorio.ObtenerDireccionClientePorIdAsync(idDireccion, idCliente);
        if (direccion == null)
            return null;

        return new DireccionClienteDTO
        {
            IdDireccionCliente = direccion.IdDireccionCliente,
            Calle = direccion.Calle,
            Numero = direccion.Numero,
            PisoDepto = direccion.PisoDepto,
            Ciudad = direccion.Ciudad,
            CodigoPostal = direccion.CodigoPostal,
            Referencia = direccion.Referencia,
            Latitud = direccion.Latitud,
            Longitud = direccion.Longitud,
            IdCliente = direccion.IdCliente
        };
    }

    public async Task<List<DireccionClienteDTO>> ObtenerDireccionesPorClienteAsync(Guid idCliente)
    {
        var direcciones = await _direccionClienteRepositorio.ObtenerDireccionesPorClienteAsync(idCliente);
        return direcciones.Select(d => new DireccionClienteDTO
        {
            IdDireccionCliente = d.IdDireccionCliente,
            Calle = d.Calle,
            Numero = d.Numero,
            PisoDepto = d.PisoDepto,
            Ciudad = d.Ciudad,
            CodigoPostal = d.CodigoPostal,
            Referencia = d.Referencia,
            Latitud = d.Latitud,
            Longitud = d.Longitud,
            IdCliente = d.IdCliente
        }).ToList();
    }
}
