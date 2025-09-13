using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    [Table("detalle_pedidos")]
    public class DetallePedido
    {
        // Claves foráneas que actúan como clave primaria compuesta
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }

        public ICollection<PedidoAdicionales> PedidoAdicionales { get; set; }
    }
}