using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    public class PedidoAdicionales
    {
        // Claves foráneas que actúan como clave primaria compuesta
        public int DetallePedidoId { get; set; }
        public int AdicionalId { get; set; }

        // Propiedades de navegación
        [ForeignKey("DetallePedidoId")]
        public DetallePedido DetallePedido { get; set; }

        [ForeignKey("AdicionalId")]
        public Adicional Adicional { get; set; }
    }
}