using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public Guid IdCliente { get; set; }

        [Column("nombre_cliente")]
        public string NombreCliente { get; set; }

        [Column("telefono_cliente")]
        public string? TelefonoCliente { get; set; }

        [Column("email_cliente")]
        public string? EmailCliente { get; set; }

        [Column("usuario")]
        public string? Usuario { get; set; }

        [Column("password_hash")]
        public string? PasswordHash { get; set; }

        [Column("password_salt")]
        public string? PasswordSalt { get; set; }

        // Propiedades de navegación para relaciones, uno a muchos o muchos a muchos.
        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<DireccionCliente> Direcciones { get; set; }
    }
}