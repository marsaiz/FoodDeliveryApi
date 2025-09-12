using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    public class DireccionCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Calle { get; set; }

        [StringLength(20)]
        public string Numero { get; set; }

        [StringLength(50)]
        public string PisoDepto { get; set; }

        public string Referencia { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Latitud { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Longitud { get; set; }

        // Clave foránea
        public int ClienteId { get; set; }

        // Propiedad de navegación
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}