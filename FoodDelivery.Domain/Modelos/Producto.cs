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
        public int IdProducto { get; set; }

        [Column ("nombre_producto")]
        public string NombreProducto { get; set; }

        [Column("descripcion_producto")]
        public string DescripcionProducto { get; set; }

        [Required]
        [Column("precio_producto")]
        public decimal PrecioProducto { get; set; }

        [Column("imagen_url")]
        public string ImagenUrl { get; set; }

        // Claves foráneas
        public int IdCategoria { get; set; }
        public Guid IdEmpresa { get; set; }

        // Propiedades de navegación
        public Categoria Categoria { get; set; }
        public Empresa Empresa { get; set; }

        // Relación muchos a muchos con DetallePedido
        public ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
