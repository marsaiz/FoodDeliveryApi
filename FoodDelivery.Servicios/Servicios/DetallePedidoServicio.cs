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
        if (detallePedidoCreateDTO.AdicionalesIds != null && detallePedidoCreateDTO.AdicionalesIds.Any())
        {
            foreach (var adicionalId in detallePedidoCreateDTO.AdicionalesIds)
            {
                // Llamar al servicio de adicionales para agregar el adicional al detalle del pedido
                await _pedidoAdicionalServicio.AgregarAdicionalADetallePedidoAsync(detalleCreado.IdPedido, detalleCreado.IdProducto, adicionalId, null, null);
            }
        }

        // 4. Retornar el DTO
        return new DetallePedidoDTO
        {
            IdPedido = detalleCreado.IdPedido,
            IdProducto = detalleCreado.IdProducto,
            Cantidad = detalleCreado.Cantidad,
            PrecioUnitario = detalleCreado.PrecioUnitario,
            AdicionalesIds = detallePedidoCreateDTO.AdicionalesIds
        };
    }

    public async Task<bool> EliminarDetallePedidoAsync(int idPedido, int idProducto)
    {
        var detalleExistente = await _detallePedidoRepositorio.ObtenerDetallePorPedidoAsync(idPedido);
        if (detalleExistente == null)
            throw new Exception("El detalle del pedido no existe.");

        return await _detallePedidoRepositorio.EliminarDetallePedidoAsync(idPedido, idProducto);
    }
}