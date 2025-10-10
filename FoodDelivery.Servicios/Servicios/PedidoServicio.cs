using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;
using FoodDelivery.Persistencia.Interfaces;

namespace FoodDelivery.Servicios.Servicios;

public class PedidoServicio : IPedidoServicio
{
    private readonly IPedidoRepositorio _pedidoRepositorio;
    private readonly IClienteServicio _clienteServicio;
    private readonly IEmpresaServicio _empresaServicio;
    private readonly IDetallePedidoServicio _detallePedidoServicio;

    public PedidoServicio(IPedidoRepositorio pedidoRepositorio, IClienteServicio clienteServicio, IEmpresaServicio empresaServicio, IDetallePedidoServicio detallePedidoServicio)
    {
        _pedidoRepositorio = pedidoRepositorio;
        _clienteServicio = clienteServicio;
        _empresaServicio = empresaServicio;
        _detallePedidoServicio = detallePedidoServicio;
    }

    public async Task<PedidoDTO> CrearPedidoAsync(PedidoCreateDTO pedidoDTO)
    {
        var cliente = await _clienteServicio.ObtenerClientePorIdAsync(pedidoDTO.IdCliente);
        if (cliente == null)
            throw new Exception("El cliente no existe.");

        var empresa = await _empresaServicio.ObtenerEmpresaPorIdAsync(pedidoDTO.IdEmpresa);
        if (empresa == null)
            throw new Exception("La empresa no existe.");

        var nuevoPedido = new Pedido
        {
            IdCliente = pedidoDTO.IdCliente,
            IdDireccionCliente = pedidoDTO.IdDireccionCliente,
            IdEmpresa = pedidoDTO.IdEmpresa,
            FechaHora = DateTime.UtcNow,
            Estado = EstadoPedido.Pendiente,
            TotalPedido = 0, // Se calculará luego
            MetodoPago = pedidoDTO.MetodoPago ?? string.Empty,
            Entrega = Enum.TryParse<TipoEntrega>(pedidoDTO.TipoEntrega, out var entrega) ? entrega : TipoEntrega.Domicilio
        };

        var pedidoCreado = await _pedidoRepositorio.CrearPedidoAsync(nuevoPedido);

        decimal totalPedido = 0;
        if (pedidoDTO.Detalles != null)
        {
            foreach (var detalle in pedidoDTO.Detalles)
            {
                // Asignar el id del pedido creado
                var detalleDto = new DetallePedidoCreateDTO
                {
                    IdProducto = detalle.IdProducto,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Adicionales = detalle.Adicionales
                };
                // El servicio de detalle debe asignar el IdPedido
                detalleDto.GetType().GetProperty("IdPedido")?.SetValue(detalleDto, pedidoCreado.IdPedido);
                var detalleCreado = await _detallePedidoServicio.CrearDetallePedidoAsync(detalleDto);
                totalPedido += (detalleCreado.PrecioUnitario ?? 0) * detalleCreado.Cantidad;
                // Sumar precio de adicionales
                if (detalle.Adicionales != null)
                {
                    foreach (var adic in detalle.Adicionales)
                    {
                        if (adic.PrecioAdicionalPersonalizado.HasValue)
                            totalPedido += adic.PrecioAdicionalPersonalizado.Value * detalle.Cantidad;
                    }
                }
            }
        }
        // Actualizar el total del pedido
        pedidoCreado.TotalPedido = totalPedido;
        await _pedidoRepositorio.ActualizarPedidoAsync(pedidoCreado);

        return new PedidoDTO
        {
            IdPedido = pedidoCreado.IdPedido,
            IdCliente = pedidoCreado.IdCliente,
            IdEmpresa = pedidoCreado.IdEmpresa,
            FechaHora = pedidoCreado.FechaHora,
            Estado = pedidoCreado.Estado,
            TotalPedido = pedidoCreado.TotalPedido
        };
    }

    public async Task<PedidoDTO> ActualizarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa, PedidoUpdateDTO pedidoDTO)
{
    var pedidoExistente = await _pedidoRepositorio.ObtenerPedidoPorIdAsync(idPedido, idCliente, idEmpresa);
    if (pedidoExistente == null)
        throw new Exception("El pedido no existe.");

    // Actualiza solo los campos permitidos
    pedidoExistente.Estado = pedidoDTO.Estado;
    pedidoExistente.MetodoPago = pedidoDTO.MetodoPago ?? pedidoExistente.MetodoPago;
    pedidoExistente.Entrega = pedidoDTO.Entrega;

    var pedidoActualizado = await _pedidoRepositorio.ActualizarPedidoAsync(pedidoExistente);
    return new PedidoDTO
    {
        IdPedido = pedidoActualizado.IdPedido,
        IdCliente = pedidoActualizado.IdCliente,
        IdEmpresa = pedidoActualizado.IdEmpresa,
        FechaHora = pedidoActualizado.FechaHora,
        Estado = pedidoActualizado.Estado,
        TotalPedido = pedidoActualizado.TotalPedido
    };
}

    public async Task<bool> EliminarPedidoAsync(int idPedido, Guid idCliente, Guid idEmpresa)
    {
        var pedidoExistente = await _pedidoRepositorio.ObtenerPedidoPorIdAsync(idPedido, idCliente, idEmpresa);
        if (pedidoExistente == null)
            throw new Exception("El pedido no existe.");

        return await _pedidoRepositorio.EliminarPedidoAsync(idPedido, idCliente, idEmpresa);
    }

    public async Task<PedidoDTO> ObtenerPedidoPorIdAsync(int idPedido, Guid idCliente, Guid idEmpresa)
    {
        var pedido = await _pedidoRepositorio.ObtenerPedidoPorIdAsync(idPedido, idCliente, idEmpresa);
        if (pedido == null) return null;

        return new PedidoDTO
        {
            IdPedido = pedido.IdPedido,
            IdCliente = pedido.IdCliente,
            IdEmpresa = pedido.IdEmpresa,
            FechaHora = pedido.FechaHora,
            Estado = pedido.Estado,
            TotalPedido = pedido.TotalPedido
        };
    }

    public async Task<List<PedidoDTO>> ObtenerPedidosPorClienteAsync(Guid idCliente)
    {
        var pedidos = await _pedidoRepositorio.ObtenerPedidosPorClienteAsync(idCliente);
        return pedidos.Select(p => new PedidoDTO
        {
            IdPedido = p.IdPedido,
            IdCliente = p.IdCliente,
            IdEmpresa = p.IdEmpresa,
            FechaHora = p.FechaHora,
            Estado = p.Estado,
            TotalPedido = p.TotalPedido
        }).ToList();
    }

    public async Task<List<PedidoDTO>> ObtenerPedidosPorEmpresaAsync(Guid idEmpresa)
    {
        var pedidos = await _pedidoRepositorio.ObtenerPedidosPorEmpresaAsync(idEmpresa);
        return pedidos.Select(p => new PedidoDTO
        {
            IdPedido = p.IdPedido,
            IdCliente = p.IdCliente,
            IdEmpresa = p.IdEmpresa,
            FechaHora = p.FechaHora,
            Estado = p.Estado,
            TotalPedido = p.TotalPedido
        }).ToList();
    }
}