using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodDelivery.Domain.Modelos
{
    [Table("direcciones_cliente")]
    public class DireccionCliente
    {
        [Key]
        [Column("id_direccion_cliente")]
        public int IdDireccionCliente { get; set; }

        [Column("calle")]
        public string Calle { get; set; }

        [Column("numero")]
        public int Numero { get; set; }

        [Column("piso_depto")]
        public string PisoDepto { get; set; }

        [Column("ciudad")]
        public string Ciudad { get; set; }

        [Column("codigo_postal")]
        public string CodigoPostal { get; set; }

        [Column("referencia")]
        public string Referencia { get; set; }

        [Column("latitud")]
        public decimal? Latitud { get; set; }

        [Column("longitud")]
        public decimal? Longitud { get; set; }

        // Clave foránea
        public Guid IdCliente { get; set; }

        // Propiedad de navegación
        public Cliente Cliente { get; set; }
    }
}