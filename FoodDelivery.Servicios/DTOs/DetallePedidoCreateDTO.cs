namespace FoodDelivery.Servicios.DTOs;

public class DetallePedidoCreateDTO
{
    public int Cantidad { get; set; }
    public int? PrecioUnitario { get; set; }
    public int IdProducto { get; set; }
    public Guid IdEmpresa { get; set; }
    public int IdPedido { get; set; }
    // Cambiar de lista de IDs a lista de objetos para soportar mitad y precio personalizado
    public List<PedidoAdicionalesCreateDTO>? Adicionales { get; set; }
}
