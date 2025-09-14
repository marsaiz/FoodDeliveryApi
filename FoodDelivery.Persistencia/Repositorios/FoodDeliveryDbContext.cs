
using FoodDelivery.Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistencia.Repositorios;

public class FoodDeliveryDbContext : DbContext
{
	public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
	{
	}

	public DbSet<Adicional> Adicionales { get; set; }
	public DbSet<Categoria> Categorias { get; set; }
	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<DetallePedido> DetallesPedido { get; set; }
	public DbSet<DireccionCliente> DireccionesCliente { get; set; }
	public DbSet<Empresa> Empresas { get; set; }
	public DbSet<Pedido> Pedidos { get; set; }
	public DbSet<PedidoAdicionales> PedidoAdicionales { get; set; }
	public DbSet<Producto> Productos { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<DetallePedido>()
            .HasKey(dp => new { dp.IdPedido, dp.IdProducto });
        modelBuilder.Entity<PedidoAdicionales>()
            .HasKey(pa => new { pa.IdPedido, pa.IdProducto, pa.IdAdicional, pa.Mitad });
		base.OnModelCreating(modelBuilder);
		// Aquí puedes agregar configuraciones adicionales si lo necesitas
	}
}
