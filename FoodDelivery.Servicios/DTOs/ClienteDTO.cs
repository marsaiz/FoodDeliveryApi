namespace FoodDelivery.Servicios.DTOs;

public class ClienteDTO
{
    public Guid? IdCliente { get; set; }
    public string NombreCliente { get; set; }
    public string? TelefonoCliente { get; set; }
    public string? EmailCliente { get; set; }
    public string? Usuario { get; set; } // <-- Útil para la autenticación
}
