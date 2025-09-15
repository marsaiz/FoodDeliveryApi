namespace FoodDelivery.Servicios.Interfaces;

public class ProductoDTO
{
    public int IdProducto { get; set; } // Solo para GET o PUT, no para POST
    public string NombreProducto { get; set; }
    public string DescripcionProducto { get; set; }
    public decimal PrecioProducto { get; set; }
    public string? ImagenUrl { get; set; } // ? Puede ser null
    public int IdCategoria { get; set; }
    public Guid IdEmpresa { get; set; }
}
