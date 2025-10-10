using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;

namespace FoodDelivery.Servicios.Servicios;

public class DetallePedidoServicio : IDetallePedidoServicio
{
    private readonly IDetallePedidoRepositorio _detallePedidoRepositorio;
    private readonly IProductoServicio _productoServicio;
    private readonly IAdicionalServicio _adicionalServicio;
    private readonly IPedidoAdicionalServicio _pedidoAdicionalServicio;

    public DetallePedidoServicio(IDetallePedidoRepositorio detallePedidoRepositorio, IProductoServicio productoServicio, IAdicionalServicio adicionalServicio, IPedidoAdicionalServicio pedidoAdicionalServicio)
    {
        _detallePedidoRepositorio = detallePedidoRepositorio;
        _productoServicio = productoServicio;
        _adicionalServicio = adicionalServicio;
        _pedidoAdicionalServicio = pedidoAdicionalServicio;
    }

    public async Task<DetallePedidoDTO> CrearDetallePedidoAsync(DetallePedidoCreateDTO detallePedidoCreateDTO)
    {
        var producto = await _productoServicio.ObtenerProductoPorIdAsync(detallePedidoCreateDTO.IdProducto, detallePedidoCreateDTO.IdEmpresa);
        if (producto == null)
            throw new Exception("El producto no existe.");

        var detallePedido = new DetallePedido
        {
            IdPedido = detallePedidoCreateDTO.IdPedido,
            IdProducto = detallePedidoCreateDTO.IdProducto,
            Cantidad = detallePedidoCreateDTO.Cantidad,
            PrecioUnitario = producto.PrecioProducto // Asignar el precio del producto al detalle del pedido. El precio sale de la tabla productos.
        };

        var detalleCreado = await _detallePedidoRepositorio.CrearDetallePedidoAsync(detallePedido);

        // 3. Manejar los adicionales si existen
        if (detallePedidoCreateDTO.Adicionales != null && detallePedidoCreateDTO.Adicionales.Any())
        {
            foreach (var adicional in detallePedidoCreateDTO.Adicionales)
            {
                // Llamar al servicio de adicionales para agregar el adicional al detalle del pedido con mitad y precio personalizado
                await _pedidoAdicionalServicio.AgregarAdicionalADetallePedidoAsync(
                    detalleCreado.IdPedido,
                    detalleCreado.IdProducto,
                    adicional.IdAdicional,
                    adicional.Mitad,
                    adicional.PrecioAdicionalPersonalizado
                );
            }
        }

        // 4. Retornar el DTO
        return new DetallePedidoDTO
        {
            IdPedido = detalleCreado.IdPedido,
            IdProducto = detalleCreado.IdProducto,
            Cantidad = detalleCreado.Cantidad,
            PrecioUnitario = detalleCreado.PrecioUnitario,
            // Puedes mapear los IDs de los adicionales si lo necesitas
            AdicionalesIds = detallePedidoCreateDTO.Adicionales?.Select(a => a.IdAdicional).ToList()
        };
    }

    public async Task<bool> EliminarDetallePedidoAsync(int idPedido, int idProducto)
    {
        var detalleExistente = await _detallePedidoRepositorio.ObtenerDetallePorPedidoAsync(idPedido);
        if (detalleExistente == null)
            throw new Exception("El detalle del pedido no existe.");

        return await _detallePedidoRepositorio.EliminarDetallePedidoAsync(idPedido, idProducto);
    }

    public async Task<DetallePedidoDTO> ActualizarDetallePedidoAsync(int idPedido, int idProducto, DetallePedidoUpdateDTO detallePedidoUpdateDTO)
    {
        var detalleExistente = (await _detallePedidoRepositorio.ObtenerDetallePorPedidoAsync(idPedido))
            .FirstOrDefault(dp => dp.IdProducto == idProducto);
        if (detalleExistente == null)
            throw new Exception("El detalle del pedido no existe.");

        // Actualizar campos
        detalleExistente.Cantidad = detallePedidoUpdateDTO.Cantidad;
        if (detallePedidoUpdateDTO.PrecioUnitario.HasValue)
            detalleExistente.PrecioUnitario = detallePedidoUpdateDTO.PrecioUnitario.Value;

        var actualizado = await _detallePedidoRepositorio.ActualizarDetallePedidoAsync(detalleExistente);

        // Nota: aquí podrías actualizar los adicionales si lo necesitas

        return new DetallePedidoDTO
        {
            IdPedido = actualizado.IdPedido,
            IdProducto = actualizado.IdProducto,
            Cantidad = actualizado.Cantidad,
            PrecioUnitario = actualizado.PrecioUnitario,
            AdicionalesIds = detallePedidoUpdateDTO.AdicionalesIds
        };
    }

    public async Task<DetallePedidoDTO> ObtenerDetallePedidoPorIdAsync(int idPedido, int idProducto)
    {
        var detalle = (await _detallePedidoRepositorio.ObtenerDetallePorPedidoAsync(idPedido))
            .FirstOrDefault(dp => dp.IdProducto == idProducto);
        if (detalle == null)
            throw new Exception("El detalle del pedido no existe.");

        return new DetallePedidoDTO
        {
            IdPedido = detalle.IdPedido,
            IdProducto = detalle.IdProducto,
            Cantidad = detalle.Cantidad,
            PrecioUnitario = detalle.PrecioUnitario,
            AdicionalesIds = detalle.PedidoAdicionales?.Select(a => a.IdAdicional).ToList() ?? new List<int>()
        };
    }

    public async Task<List<DetallePedidoDTO>> ObtenerDetallesPorPedidoAsync(int idPedido)
    {
        var detalles = await _detallePedidoRepositorio.ObtenerDetallePorPedidoAsync(idPedido);
        return detalles.Select(detalle => new DetallePedidoDTO
        {
            IdPedido = detalle.IdPedido,
            IdProducto = detalle.IdProducto,
            Cantidad = detalle.Cantidad,
            PrecioUnitario = detalle.PrecioUnitario,
            AdicionalesIds = detalle.PedidoAdicionales?.Select(a => a.IdAdicional).ToList() ?? new List<int>()
        }).ToList();
    }
}