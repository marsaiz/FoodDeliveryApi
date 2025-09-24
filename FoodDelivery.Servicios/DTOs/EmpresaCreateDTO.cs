namespace FoodDelivery.Servicios.DTOs;

public class EmpresaCreateDTO
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public string Usuario { get; set; } // Para registrarse en el sistema
    public string Password { get; set; }
}