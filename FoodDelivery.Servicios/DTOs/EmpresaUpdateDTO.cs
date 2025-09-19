namespace FoodDelivery.Servicios.DTOs;

public class EmpresaUpdateDTO
{
    public Guid IdEmpresa { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
}
