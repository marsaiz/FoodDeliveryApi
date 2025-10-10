public class PedidoAdicionalesCreateDTO
{
    public int IdAdicional { get; set; }
    public int Mitad { get; set; } // 0: completo, 1: 1ra mitad, 2: 2da mitad
    public decimal? PrecioAdicionalPersonalizado { get; set; }
}
