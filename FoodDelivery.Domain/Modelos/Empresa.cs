using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace FoodDelivery.Domain.Modelos
{
    [Table("empresas")]
    public class Empresa
    {
        [Key]
        [Column("id_empresa")]
        public Guid IdEmpresa { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("latitud")]
        public decimal? Latitud { get; set; }

        [Column("longitud")]
        public decimal? Longitud { get; set; }

        [Column("esta_abierta")]
        public bool EstaAbierta { get; set; } = true;

        [Column("usuario")]
        public string? Usuario { get; set; }
        [Column("password_hash")]
        public string? PasswordHash { get; set; }
        
        // Propiedades de navegación para relaciones
        // ICollection<T> indica una relación uno a muchos o muchos a muchos
        // la coleccion debe estar en la entidad "uno" en una relación uno a muchos
        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public ICollection<Adicional> Adicionales { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}