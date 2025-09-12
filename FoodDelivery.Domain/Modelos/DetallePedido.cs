using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    public class DetallePedido
    {
        // Claves foráneas que actúan como clave primaria compuesta
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        public ICollection<PedidoAdicionales> PedidoAdicionales { get; set; }
    }
}