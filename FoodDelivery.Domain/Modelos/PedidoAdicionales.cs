using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    [Table("pedido_adicionales")]
    public class PedidoAdicionales
    {
        // Claves foráneas que actúan como clave primaria compuesta
        public int DetallePedidoId { get; set; }
        public int IdAdicional { get; set; }

        // Propiedades de navegación
        public DetallePedido DetallePedido { get; set; }
        public Adicional Adicional { get; set; }
    }
}