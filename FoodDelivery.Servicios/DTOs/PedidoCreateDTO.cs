using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.DTOs;

using System.Collections.Generic;

public class PedidoCreateDTO
{
    public Guid IdCliente { get; set; }
    public Guid IdEmpresa { get; set; }
    public string? MetodoPago { get; set; }
    public string? TipoEntrega { get; set; }
    public int? IdDireccionCliente { get; set; }
    public List<DetallePedidoCreateDTO>? Detalles { get; set; }
}
