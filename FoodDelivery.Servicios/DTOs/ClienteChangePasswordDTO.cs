namespace FoodDelivery.Servicios.DTOs;

public class ClienteChangePasswordDTO
{
    public string PasswordActual { get; set; } // Para verificar la contraseña actual
    public string NuevoPassword { get; set; } // La nueva contraseña que se desea establecer
}
