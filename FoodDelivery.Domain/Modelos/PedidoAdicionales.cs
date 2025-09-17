using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    [Table("pedido_adicionales")]
    public class PedidoAdicionales
    {
        // Clave compuesta: DetallePedidoId + IdAdicional
        [Column("id_pedido")]
        public int IdPedido { get; set; } // Clave foránea

        [Column("id_producto")]
        public int IdProducto { get; set; } // Clave foránea parte de la clave compuesta

        // Clave foránea a Adicional
        [Column("id_adicional")]
        public int IdAdicional { get; set; }

        [Column("mitad")]
        public int? Mitad { get; set; } // 1: primera mitad, 2: segunda mitad, null: adicional completo

        // Nuevo campo para el precio personalizado
        [Column("precio_adicional_personalizado")]
        public decimal? PrecioAdicionalPersonalizado { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdPedido")]
        public DetallePedido DetallePedido { get; set; }

        [ForeignKey("IdAdicional")]
        public Adicional Adicional { get; set; }

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }
    }
}