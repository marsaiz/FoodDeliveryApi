namespace FoodDelivery.Servicios.DTOs;

public class DireccionClienteDTO
{
    public int? IdDireccionCliente { get; set; }
    public string Calle { get; set; }
    public int Numero { get; set; }
    public string? PisoDepto { get; set; }
    public string Ciudad { get; set; }
    public string CodigoPostal { get; set; }
    public string? Referencia { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public Guid IdCliente { get; set; }
}
