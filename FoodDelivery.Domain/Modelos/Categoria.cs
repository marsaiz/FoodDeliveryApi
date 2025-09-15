using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Required]
        [Column("nombre_categoria")]
        public string NombreCategoria { get; set; }

        // Clave foránea
        public Guid IdEmpresa { get; set; }

        // Propiedades de navegación
        public Empresa Empresa { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}