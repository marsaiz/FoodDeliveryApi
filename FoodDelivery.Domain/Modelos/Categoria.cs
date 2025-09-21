using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Required]
        [Column("nombre_categoria")]
        public string NombreCategoria { get; set; }

        // Clave foránea
        [Column("id_empresa")]
        public Guid IdEmpresa { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}