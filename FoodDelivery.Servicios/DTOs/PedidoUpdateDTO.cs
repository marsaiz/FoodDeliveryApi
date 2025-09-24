using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.DTOs;

public class PedidoUpdateDTO
{
    public EstadoPedido Estado { get; set; } // Enum, no string
    public string? MetodoPago { get; set; }
    public TipoEntrega Entrega { get; set; }
}
