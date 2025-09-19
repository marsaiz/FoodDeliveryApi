namespace FoodDelivery.Servicios.DTOs;

public class ProductoUpdateDTO
{
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public string DescripcionProducto { get; set; }
    public decimal PrecioProducto { get; set; }
    public string? ImagenUrl { get; set; }
    public int IdCategoria { get; set; }
    public Guid IdEmpresa { get; set; }
}
