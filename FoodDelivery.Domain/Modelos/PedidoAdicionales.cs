using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    [Table("pedido_adicionales")]
    public class PedidoAdicionales
    {
        // Clave compuesta: DetallePedidoId + IdAdicional + Mitad
        public int IdPedido { get; set; } // Clave foránea
        // public int IdProducto { get; set; }

        // Clave foránea a Adicional
        public int IdAdicional { get; set; }

        [Column("mitad")]
        public int? Mitad { get; set; } // 1: primera mitad, 2: segunda mitad, null: adicional completo

        // Nuevo campo para el precio personalizado
        [Column("precio_adicional_personalizado")]
        public decimal? PrecioAdicionalPersonalizado { get; set; }

        // Propiedades de navegación
        public DetallePedido DetallePedido { get; set; }
        public Adicional Adicional { get; set; }
    }
}