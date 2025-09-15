namespace FoodDelivery.Servicios.Interfaces;

public class AdicionalDTO
{
    public int? IdAdicional { get; set; } // null para crear, valor para actualizar/consultar
    public string NombreAdicional { get; set; }
    public decimal? PrecioAdicional { get; set; }
    public Guid EmpresaId { get; set; }
}
