using FoodDelivery.Domain.Modelos;
using FoodDelivery.Servicios.Interfaces;
using FoodDelivery.Servicios.DTOs;

namespace FoodDelivery.Servicios.Servicios;

public class PedidoServicio : IPedidoServicio
{
    private readonly IPedidoRepositorio _pedidoRepositorio;
    private readonly IClienteRepositorio _clienteRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public PedidoServicio(IPedidoRepositorio pedidoRepositorio, IClienteRepositorio clienteRepositorio, IEmpresaRepositorio empresaRepositorio)
    {
        _pedidoRepositorio = pedidoRepositorio;
        _clienteRepositorio = clienteRepositorio;
        _empresaRepositorio = empresaRepositorio;
    }

    public async Task<Pedido> CrearPedidoAsync(PedidoCreateDTO pedidoDTO)
    {
        var cliente = await _clienteRepositorio.ObtenerClientePorIdAsync(pedidoDTO.IdCliente);
        if (cliente == null)
            throw new Exception("El cliente no existe.");

        var empresa = await _empresaRepositorio.ObtenerEmpresaPorIdAsync(pedidoDTO.IdEmpresa);
        if (empresa == null)
            throw new Exception("La empresa no existe.");

        var nuevoPedido = new Pedido
        {
            IdCliente = pedidoDTO.IdCliente,
            IdEmpresa = pedidoDTO.IdEmpresa,
            FechaPedido = DateTime.UtcNow,
            EstadoPedido = "Pendiente",
            TotalPedido = pedidoDTO.TotalPedido
        };

        return await _pedidoRepositorio.CrearPedidoAsync(nuevoPedido);
    }

    public async Task<Pedido> ActualizarEstadoPedidoAsync(int idPedido, string nuevoEstado)
    {
        var pedidoExistente = await _pedidoRepositorio.ObtenerPedidoPorIdAsync(idPedido);
        if (pedidoExistente == null)
            throw new Exception("El pedido no existe.");

        pedidoExistente.EstadoPedido = nuevoEstado;
        return await _pedidoRepositorio.ActualizarPedidoAsync(pedidoExistente);
    }

    public async Task<bool> EliminarPedidoAsync(int idPedido)
    {
        var pedidoExistente = await _pedidoRepositorio.ObtenerPedidoPorIdAsync(idPedido);
        if (pedidoExistente == null)
            throw new Exception("El pedido no existe.");

        return await _pedidoRepositorio.EliminarPedidoAsync(idPedido);
    }

    public async Task<PedidoDTO> ObtenerPedidoPorIdAsync(int idPedido)
    {
        var pedido = await _pedidoRepositorio.ObtenerPedidoPorIdAsync(idPedido);
        if (pedido == null) return null;

        return new PedidoDTO
        {
            IdPedido = pedido.IdPedido,
            IdCliente = pedido.IdCliente,
            IdEmpresa = pedido.IdEmpresa,
            FechaPedido = pedido.FechaPedido,
            EstadoPedido = pedido.EstadoPedido,
            TotalPedido = pedido.TotalPedido
        };
    }
}