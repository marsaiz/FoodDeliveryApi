using System.Collections.Generic;
using FoodDelivery.Servicios.DTOs;

public class DetallePedidoCreateDTO
{
    public int IdPedido { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int IdProducto { get; set; }
    public List<PedidoAdicionalesCreateDTO>? Adicionales { get; set; }
}
