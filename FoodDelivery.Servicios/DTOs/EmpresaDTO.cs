namespace FoodDelivery.Servicios.DTOs;

public class EmpresaDTO
{
    public Guid? IdEmpresa { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public bool EstaAbierta { get; set; } = true; // <-- Útil para la aplicación móvil
    public string? Usuario { get; set; } // <-- Útil para la autenticación
}
