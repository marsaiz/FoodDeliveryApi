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

    public DetallePedidoServicio(IDetallePedidoRepositorio detallePedidoRepositorio, IProductoServicio productoServicio, IAdicionalServicio adicionalServicio)
    {
        _detallePedidoRepositorio = detallePedidoRepositorio;
        _productoServicio = productoServicio;
        _adicionalServicio = adicionalServicio;
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
            PrecioUnitario = producto.PrecioProducto // Asignar el precio del producto al detalle del pedido
        };

        var detalleCreado = await _detallePedidoRepositorio.CrearDetallePedidoAsync(detallePedido);

        // 3. Manejar los adicionales si existen
        if (detallePedidoCreateDTO.AdicionalesIds != null && detallePedidoCreateDTO.AdicionalesIds.Any())
        {
            foreach (var adicionalId in detallePedidoCreateDTO.AdicionalesIds)
            {
                // Aquí llamas al servicio/repositorio para crear el registro en PedidoAdicionales
                await _adicionalServicio.CrearAdicionalAsync(new AdicionalCreateDTO
                {
                    IdDetallePedido = detalleCreado.IdPedido, // Ajusta según tu modelo
                    IdProducto = detalleCreado.IdProducto,
                    IdEmpresa = detallePedidoCreateDTO.IdEmpresa,
                    NombreAdicional = "Adicional desde DetallePedido", // Puedes ajustar esto según tus necesidades
                    PrecioAdicional = 0 // Ajusta según sea necesario
                });
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


  
}