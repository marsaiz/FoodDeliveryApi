namespace FoodDelivery.Servicios.DTOs;

public class DetallePedidoCreateDTO
{
    public int Cantidad { get; set; }
    public decimal? PrecioUnitario { get; set; }
    public int IdProducto { get; set; }
    public int IdPedido { get; set; }
    public List<int> AdicionalesIds { get; set; }
}
