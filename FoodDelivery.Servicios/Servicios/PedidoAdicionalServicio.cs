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
            IdPedido = pa.IdPedido,
            IdProducto = pa.IdProducto,
            IdAdicional = pa.IdAdicional,
            Mitad = pa.Mitad,
            PrecioAdicionalPersonalizado = pa.PrecioAdicionalPersonalizado
        }).ToList();
    }

    public async Task<PedidoAdicionalesDTO> CrearPedidoAdicionalAsync(PedidoAdicionalesCreateDTO pedidoAdicionalesCreateDTO)
    {
        var pedidoAdicionales = new PedidoAdicionales
        {
            IdPedido = pedidoAdicionalesCreateDTO.IdPedido,
            IdAdicional = pedidoAdicionalesCreateDTO.IdAdicional,
            Mitad = pedidoAdicionalesCreateDTO.Mitad,
            PrecioAdicionalPersonalizado = pedidoAdicionalesCreateDTO.PrecioAdicionalPersonalizado
        };

        var creado = await _pedidoAdicionalesRepositorio.CrearPedidoAdicionalAsync(pedidoAdicionales);

        return new PedidoAdicionalesDTO
        {
            IdPedido = creado.IdPedido,
            IdProducto = creado.IdProducto,
            IdAdicional = creado.IdAdicional,
            Mitad = creado.Mitad,
            PrecioAdicionalPersonalizado = creado.PrecioAdicionalPersonalizado
        };
    }

    public async Task<PedidoAdicionalesDTO> ActualizarPedidoAdicionalAsync(PedidoAdicionalesUpdateDTO pedidoAdicionalesUpdateDTO)
    {
        var pedidoAdicionales = new PedidoAdicionales
        {
            IdPedido = pedidoAdicionalesUpdateDTO.IdPedido,
            IdProducto = pedidoAdicionalesUpdateDTO.IdProducto,
            IdAdicional = pedidoAdicionalesUpdateDTO.IdAdicional,
            Mitad = pedidoAdicionalesUpdateDTO.Mitad,
            PrecioAdicionalPersonalizado = pedidoAdicionalesUpdateDTO.PrecioAdicionalPersonalizado
        };

        var actualizado = await _pedidoAdicionalesRepositorio.ActualizarPedidoAdicionalAsync(pedidoAdicionales);

        return new PedidoAdicionalesDTO
        {
            IdPedido = actualizado.IdPedido,
            IdProducto = actualizado.IdProducto,
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
