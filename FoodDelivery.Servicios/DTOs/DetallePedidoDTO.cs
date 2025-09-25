namespace FoodDelivery.Servicios.DTOs;

public class DetallePedidoDTO
{
    public int Cantidad { get; set; }
    public decimal? PrecioUnitario { get; set; }
    public int IdPedido { get; set; }
    public int IdProducto { get; set; }
    public List<int> AdicionalesIds { get; set; } // IDs de los adicionales seleccionados
}
