using Microsoft.EntityFrameworkCore;
using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Repositorios;

public class FoodDeliveryContexto : DbContext
{
    public FoodDeliveryContexto(DbContextOptions<FoodDeliveryContexto> options) : base(options)
    {
    }

    // DbSets para las entidades
    public DbSet<Adicional> Adicionales { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<DireccionCliente> Direcciones { get; set; }
    public DbSet<DetallePedido> DetallePedidos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoAdicionales> PedidoAdicionales { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación muchos a muchos entre Pedido y Adicional a través de PedidoAdicionales
        modelBuilder.Entity<PedidoAdicionales>()
            .HasKey(pa => new { pa.IdPedido, pa.IdAdicional, pa.Mitad });

        modelBuilder.Entity<PedidoAdicionales>()
            .HasOne<Pedido>()
            .WithMany(p => p.PedidosAdicionales)
            .HasForeignKey(pa => pa.IdPedido);

        modelBuilder.Entity<PedidoAdicionales>()
            .HasOne<Adicional>()
            .WithMany(a => a.PedidoAdicionales)
            .HasForeignKey(pa => pa.IdAdicional);
    }
}
