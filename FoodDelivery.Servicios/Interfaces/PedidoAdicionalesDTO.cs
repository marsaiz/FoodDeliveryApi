namespace FoodDelivery.Servicios.Interfaces;

public class PedidoAdicionalesDTO
{
    public int? IdProducto { get; set; }
    public int IdPedido { get; set; }
    public int IdAdiconal { get; set; }
    public int? Mitad { get; set; }
    public decimal? PreacioAdicionalPersonalizado { get; set; } 
    // public List<int> AdicionalesIds { get; set; }
}
