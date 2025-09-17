using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Modelos
{
    [Table("adicionales")]
    public class Adicional
    {
        [Key]
        [Column("id_adicionales")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdicional { get; set; }

        [Required]
        [Column ("nombre_adicional")]
        public string NombreAdicional { get; set; }

        [Column("precio_adicional")]
        public decimal? PrecioAdicional { get; set; }

        // Clave foránea
        [Column("id_empresa")]
        public Guid IdEmpresa { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }
        public ICollection<PedidoAdicionales> PedidoAdicionales { get; set; }
    }
}