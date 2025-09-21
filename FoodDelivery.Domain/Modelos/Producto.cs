using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Column("nombre_producto")]
        public string NombreProducto { get; set; }

        [Column("descripcion_producto")]
        public string DescripcionProducto { get; set; }

        [Column("precio_producto")]
        public decimal PrecioProducto { get; set; }

        [Column("imagen_url")]
        public string? ImagenUrl { get; set; }

        // Claves foráneas
        [Column("id_categoria")]
        public int IdCategoria { get; set; }
        [Column("id_empresa")]
        public Guid IdEmpresa { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }

        // Relación muchos a muchos con DetallePedido
        public ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
