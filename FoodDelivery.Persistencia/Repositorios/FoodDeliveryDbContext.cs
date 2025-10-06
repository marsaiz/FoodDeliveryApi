using Microsoft.EntityFrameworkCore;
using FoodDelivery.Domain.Modelos;

namespace FoodDelivery.Persistencia.Repositorios;

public class FoodDeliveryDbContext : DbContext
{
    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
    {
    }

    // DbSets para las entidades
    public DbSet<Adicional> Adicionales { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<DireccionCliente> DireccionesClientes { get; set; }
    public DbSet<DetallePedido> DetallePedidos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoAdicionales> PedidoAdicionales { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Clave primaria compuesta para DetallePedido
        modelBuilder.Entity<DetallePedido>()
            .HasKey(dp => new { dp.IdPedido, dp.IdProducto });

        // Clave primaria compuesta para PedidoAdicionales
        modelBuilder.Entity<PedidoAdicionales>()
            .HasKey(pa => new { pa.IdPedido, pa.IdProducto, pa.IdAdicional });

        // Relación PedidoAdicionales -> DetallePedido usando la clave compuesta
        modelBuilder.Entity<PedidoAdicionales>()
            .HasOne(pa => pa.DetallePedido)
            .WithMany(dp => dp.PedidoAdicionales)
            .HasForeignKey(pa => new { pa.IdPedido, pa.IdProducto });

        // Relación PedidoAdicionales -> Adicional
        modelBuilder.Entity<PedidoAdicionales>()
            .HasOne(pa => pa.Adicional)
            .WithMany(a => a.PedidoAdicionales)
            .HasForeignKey(pa => pa.IdAdicional);

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Estado)
            .HasConversion<string>();

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Entrega)
            .HasConversion<string>();
    }
}
