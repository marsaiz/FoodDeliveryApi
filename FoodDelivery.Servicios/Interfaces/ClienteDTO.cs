namespace FoodDelivery.Servicios.Interfaces;

public class ClienteDTO
{
    public Guid? IdCliente { get; set; }
    public string Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    
}
