using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    [Table("pedido_adicionales")]
    public class PedidoAdicionales
    {

        // Clave compuesta: IdDetallePedido + IdAdicional
        [Key, Column(Order = 0)]
        //[Column("id_pedido")]
        public int IdPedido { get; set; } // Clave foránea a DetallePedido

        [Key, Column(Order = 1)]
        public int IdProducto { get; set; } // Clave foránea a DetallePedido

        // Clave foránea a Adicional
        [Key, Column(Order = 2)]
        //[Column("id_adicional")]
        public int IdAdicional { get; set; }

        [Column("mitad")]
        public int? Mitad { get; set; } // 0: sin mitad, 1: primera mitad, 2: segunda mitad, null: adicional completo

        // Nuevo campo para el precio personalizado
        [Column("precio_adicional_personalizado")]
        public decimal? PrecioAdicionalPersonalizado { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdPedido,IdProducto")]
        public DetallePedido? DetallePedido { get; set; }

        [ForeignKey("IdAdicional")]
        public Adicional? Adicional { get; set; }
    }
}