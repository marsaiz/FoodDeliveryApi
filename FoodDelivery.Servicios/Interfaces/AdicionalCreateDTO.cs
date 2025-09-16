namespace FoodDelivery.Servicios.Interfaces;

public class AdicionalCreateDTO
{
    public string NombreAdicional { get; set; }
    public decimal? PrecioAdicional { get; set; }
    public Guid IdEmpresa { get; set; }
}