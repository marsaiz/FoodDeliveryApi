using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        // Propiedades de navegación
        public ICollection<DireccionCliente> Direcciones { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}