
namespace FoodDelivery.Servicios.DTOs;

public class ClienteUpdateDTO
{
    public Guid IdCliente { get; set; }
    public string? NombreCliente { get; set; }
    public string? TelefonoCliente { get; set; }
    public string? EmailCliente { get; set; }
}
