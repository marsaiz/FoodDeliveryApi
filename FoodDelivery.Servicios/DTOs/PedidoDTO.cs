using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.DTOs;

public class PedidoDTO
{
    public int? IdPedido { get; set; }
    public DateTime FechaHora { get; set; }
    public decimal TotalPedido { get; set; }
    public string MetodoPago { get; set; }
    public string TipoEntrega { get; set; }
    public Guid? IdCliente { get; set; }
    public int IdDireccionCliente { get; set; }
    public Guid EmpresaId { get; set; }
    public EstadoPedido Estado { get; set; }

    // public List<DetallePedidoDTO> Detalles { get; set; }
}
