using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Servicios.DTOs;

public class PedidoAdicionalesCreateDTO
{
    public int IdPedido { get; set; }
    public int IdProducto { get; set; }
    public int IdAdicional { get; set; }
    public int? Mitad { get; set; }
    public decimal? PrecioAdicionalPersonalizado { get; set; } 
    public List<int> AdicionalesIds { get; set; }
}
