using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Domain.Modelos
{
    [Table("detalle_pedidos")]
    public class DetallePedido
    {
        // Claves foráneas que actúan como clave primaria compuesta
        [Column("id_pedido")]
        public int IdPedido { get; set; }
        
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdPedido")]
        public Pedido Pedido { get; set; }  

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }

        public ICollection<PedidoAdicionales> PedidoAdicionales { get; set; }
    }
}