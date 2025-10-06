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

    public async Task<List<PedidoAdicionalesDTO>> ObtenerPedidoAdicional(int idPedido, int idProducto)
    {
        var adicionales = await _pedidoAdicionalesRepositorio.ObtenerPedidoAdicional(idPedido, idProducto);
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
            IdProducto = pedidoAdicionalesCreateDTO.IdProducto,
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
            PrecioAdicionalPersonalizado = pedidoAdicionalesUpdateDTEliminarAsyncO.PrecioAdicionalPersonalizado
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

    public async Task<bool> EliminarAsync(int idPedidoAdicional, int idAdicional)
    {
        return await _pedidoAdicionalesRepositorio.EliminarAsync(idPedidoAdicional, idAdicional);
    }

    public async Task<AdicionalDTO> AgregarAdicionalADetallePedidoAsync(int idPedido, int idProducto, int idAdicional, int? mitad, decimal? precioPersonalizado)
    {
        var pedidoAdicional = new PedidoAdicionales
        {
            IdPedido = idPedido,
            IdProducto = idProducto,
            IdAdicional = idAdicional,
            Mitad = mitad,
            PrecioAdicionalPersonalizado = precioPersonalizado
        };
        var creado = await _pedidoAdicionalesRepositorio.CrearPedidoAdicionalAsync(pedidoAdicional);
        // Retornar un DTO del adicional asociado
        return new AdicionalDTO
        {
            IdAdicional = creado.IdAdicional,
            NombreAdicional = creado.Adicional?.NombreAdicional,
            PrecioAdicional = creado.PrecioAdicionalPersonalizado ?? creado.Adicional?.PrecioAdicional
        };
    }
}
