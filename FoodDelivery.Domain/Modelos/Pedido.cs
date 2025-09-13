using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;


namespace FoodDelivery.Domain.Modelos
{
    [Table("pedidos")]
    public class Pedido
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; }

        [Column("estado_pedido")]
        public string EstadoPedido { get; set; }

        [Column ("total_pedido")]
        public decimal TotalPeido { get; set; }

        [Column("metodo_pago")]
        public string MetodoPago { get; set; }

        [Column("tipo_entrega")]
        public string TipoEntrega { get; set; }

        // Claves foráneas
        public Guid IdCliente { get; set; }
        public Guid IdEmpresa { get; set; }
        public int? IdDireccionCliente { get; set; }

        // Propiedades de navegación 
        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public DireccionCliente DireccionEntrega { get; set; }
        public ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}