using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Servicios.DTOs;

public class ProductoCreateDTO
{
    [Required]
    public string NombreProducto { get; set; }
    [Required]
    public string DescripcionProducto { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")] // Validación para precio positivo
    public decimal PrecioProducto { get; set; }
    public string? ImagenUrl { get; set; }
    public int IdCategoria { get; set; }
    public Guid IdEmpresa { get; set; }
    public bool Activo { get; set; } = true;
    public int CantidadDisponible { get; set; }
}
