namespace FoodDelivery.Servicios.DTOs;

public class AdicionalDTO
{
    public int? IdAdicional { get; set; } // null para crear, valor para actualizar/consultar
    public string NombreAdicional { get; set; }
    public decimal? PrecioAdicional { get; set; }
    public Guid IdEmpresa { get; set; }
}
