using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }

        // Propiedades de navegación para relaciones
        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public ICollection<Adicional> Adicionales { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}