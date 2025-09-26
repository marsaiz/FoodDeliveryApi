using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Domain.Modelos;
using FoodDelivery.Persistencia.Interfaces;

namespace FoodDelivery.Servicios.Servicios;

public class PedidoAdicionalServicio : IPedidoAdicionalServicio
{
    private readonly IPedidoAdicionalesRepositorio _pedidoAdicionalesRepositorio;

    public PedidoAdicionalServicio(IPedidoAdicionalesRepositorio pedidoAdicionalesRepositorio)
    {
        _pedidoAdicionalesRepositorio = pedidoAdicionalesRepositorio;
    }

    public async Task<List<PedidoAdicionalesDTO>> ObtenerPedidoAdicional(int idAdicional, int idDetallePedido)
    {
        var adicionales = await _pedidoAdicionalesRepositorio.ObtenerPedidoAdicional(idAdicional, idDetallePedido);
        return adicionales.Select(pa => new PedidoAdicionalesDTO
        {
            IdDetallePedido = pa.IdDetallePedido,
            IdAdicional = pa.IdAdicional,
            Mitad = pa.Mitad,
            PrecioAdicionalPersonalizado = pa.PrecioAdicionalPersonalizado
        }).ToList();
    }

    public async Task<PedidoAdicionalesDTO> CrearPedidoAdicionalAsync(PedidoAdicionalesCreateDTO pedidoAdicionalesCreateDTO)
    {
        var pedidoAdicionales = new PedidoAdicionales
        {
            IdDetallePedido = pedidoAdicionalesCreateDTO.IdDetallePedido,
            IdAdicional = pedidoAdicionalesCreateDTO.IdAdicional,
            Mitad = pedidoAdicionalesCreateDTO.Mitad,
            PrecioAdicionalPersonalizado = pedidoAdicionalesCreateDTO.PrecioAdicionalPersonalizado
        };

        var creado = await _pedidoAdicionalesRepositorio.CrearPedidoAdicionalAsync(pedidoAdicionales);

        return new PedidoAdicionalesDTO
        {
            IdDetallePedido = creado.IdDetallePedido,
            IdAdicional = creado.IdAdicional,
            Mitad = creado.Mitad,
            PrecioAdicionalPersonalizado = creado.PrecioAdicionalPersonalizado
        };
    }

    public async Task<PedidoAdicionalesDTO> ActualizarPedidoAdicionalAsync(PedidoAdicionalesUpdateDTO pedidoAdicionalesUpdateDTO)
    {
        var pedidoAdicionales = new PedidoAdicionales
        {
            IdDetallePedido = pedidoAdicionalesUpdateDTO.IdDetallePedido,
            IdAdicional = pedidoAdicionalesUpdateDTO.IdAdicional,
            Mitad = pedidoAdicionalesUpdateDTO.Mitad,
            PrecioAdicionalPersonalizado = pedidoAdicionalesUpdateDTO.PrecioAdicionalPersonalizado
        };

        var actualizado = await _pedidoAdicionalesRepositorio.ActualizarPedidoAdicionalAsync(pedidoAdicionales);

        return new PedidoAdicionalesDTO
        {
            IdDetallePedido = actualizado.IdDetallePedido,
            IdAdicional = actualizado.IdAdicional,
            Mitad = actualizado.Mitad,
            PrecioAdicionalPersonalizado = actualizado.PrecioAdicionalPersonalizado
        };
    }

    public async Task<bool> EliminarAsync(int idDetallePedido, int idAdicional)
    {
        return await _pedidoAdicionalesRepositorio.EliminarAsync(idDetallePedido, idAdicional);
    }
}
