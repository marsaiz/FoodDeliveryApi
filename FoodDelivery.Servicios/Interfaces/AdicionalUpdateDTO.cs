namespace FoodDelivery.Servicios.Interfaces;

public class AdicionalUpdateDTO
{
    public int IdAdicional { get; set; }
    public string NombreAdicional { get; set; }
    public decimal? PrecioAdicional { get; set; }
    public Guid IdEmpresa { get; set; }
}