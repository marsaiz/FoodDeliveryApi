using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;


namespace FoodDelivery.Domain.Modelos
{

    public enum EstadoPedido
    {
        Pendiente,
        EnPreparacion,
        EnCamino,
        Entregado,
        Cancelado
    }

    [Table("pedidos")]
    public class Pedido
    {
        [Key]
        [Column("id_pedido")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }

        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; }

        [Column("total_pedido")]
        public decimal TotalPeido { get; set; }

        [Column("metodo_pago")]
        public string MetodoPago { get; set; }

        [Column("tipo_entrega")]
        public string TipoEntrega { get; set; }

        [Required]
        [Column("estado_pedido")]
        public EstadoPedido Estado { get; set; }

        // Claves foráneas
        [Column("id_cliente")]
        public Guid IdCliente { get; set; }
        [Column("id_empresa")]
        public Guid IdEmpresa { get; set; }
        [Column("id_direccion_cliente")]
        public int? IdDireccionCliente { get; set; }

        // Propiedades de navegación 
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }
        [ForeignKey("IdDireccionCliente")]
        public DireccionCliente DireccionEntrega { get; set; }

        // ICollection<T> indica una relación uno a muchos o muchos a muchos
        // la coleccion debe estar en la entidad "uno" en una relación uno a muchos
        // o en ambas entidades en una relación muchos a muchos
        public ICollection<DetallePedido> DetallePedidos { get; set; }
        public ICollection<PedidoAdicionales> PedidosAdicionales { get; set; }
    }
}