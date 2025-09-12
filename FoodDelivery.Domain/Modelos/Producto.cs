using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        public string ImagenUrl { get; set; }

        // Claves foráneas
        public int CategoriaId { get; set; }
        public int EmpresaId { get; set; }

        // Propiedades de navegación
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        public ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
