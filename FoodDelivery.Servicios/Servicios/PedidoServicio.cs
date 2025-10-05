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

    public PedidoServicio(IPedidoRepositorio pedidoRepositorio, IClienteServicio clienteServicio, IEmpresaServicio empresaServicio)
    {
        _pedidoRepositorio = pedidoRepositorio;
        _clienteServicio = clienteServicio;
        _empresaServicio = empresaServicio;
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
            TotalPedido = pedidoDTO.TotalPedido,
            MetodoPago = pedidoDTO.MetodoPago,
            Entrega = pedidoDTO.Entrega
        };

        var pedidoCreado = await _pedidoRepositorio.CrearPedidoAsync(nuevoPedido);
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