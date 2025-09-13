using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Clave foránea
        public int EmpresaId { get; set; }

        // Propiedades de navegación
        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}