namespace FoodDelivery.Servicios.DTOs;

public class PedidoUpdateDTO
{
    public DateTime FechaHora { get; set; }
    public decimal TotalPedido { get; set; }
    public string MetodoPago { get; set; }
    public string TipoEntrega { get; set; }
    public string EstadoPedido { get; set; }
}
