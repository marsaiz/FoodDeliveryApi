using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;


namespace FoodDelivery.Domain.Modelos
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPago { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoEntrega { get; set; }

        // Claves foráneas
        public int ClienteId { get; set; }
        public int EmpresaId { get; set; }
        public int? DireccionEntregaId { get; set; }

        // Propiedades de navegación
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        [ForeignKey("DireccionEntregaId")]
        public DireccionCliente DireccionEntrega { get; set; }

        public ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}