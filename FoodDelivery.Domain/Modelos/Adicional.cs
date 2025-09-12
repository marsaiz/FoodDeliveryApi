using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Modelos
{
    public class Adicional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Precio { get; set; }

        // Clave foránea
        public int EmpresaId { get; set; }

        // Propiedades de navegación
        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        public ICollection<PedidoAdicionales> PedidoAdicionales { get; set; }
    }
}