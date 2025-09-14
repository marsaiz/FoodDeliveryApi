using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id_cliente = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    telefono = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id_empresa = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    direccion = table.Column<string>(type: "text", nullable: false),
                    telefono = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    latitud = table.Column<decimal>(type: "numeric", nullable: true),
                    longitud = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id_empresa);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_cliente",
                columns: table => new
                {
                    id_direccion_cliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    calle = table.Column<string>(type: "text", nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    piso_depto = table.Column<string>(type: "text", nullable: false),
                    ciudad = table.Column<string>(type: "text", nullable: false),
                    codigo_postal = table.Column<string>(type: "text", nullable: false),
                    referencia = table.Column<string>(type: "text", nullable: false),
                    latitud = table.Column<decimal>(type: "numeric", nullable: true),
                    longitud = table.Column<decimal>(type: "numeric", nullable: true),
                    IdCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteIdCliente = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones_cliente", x => x.id_direccion_cliente);
                    table.ForeignKey(
                        name: "FK_direcciones_cliente_clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "adicionales",
                columns: table => new
                {
                    id_adicionales = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_adicional = table.Column<string>(type: "text", nullable: false),
                    precio_adicional = table.Column<decimal>(type: "numeric", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adicionales", x => x.id_adicionales);
                    table.ForeignKey(
                        name: "FK_adicionales_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_categoria = table.Column<string>(type: "text", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id_categoria);
                    table.ForeignKey(
                        name: "FK_categorias_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id_pedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado_pedido = table.Column<string>(type: "text", nullable: false),
                    total_pedido = table.Column<decimal>(type: "numeric", nullable: false),
                    metodo_pago = table.Column<string>(type: "text", nullable: false),
                    tipo_entrega = table.Column<string>(type: "text", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEmpresa = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDireccionCliente = table.Column<int>(type: "integer", nullable: true),
                    ClienteIdCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaIdEmpresa = table.Column<Guid>(type: "uuid", nullable: false),
                    DireccionEntregaIdDireccionCliente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id_pedido);
                    table.ForeignKey(
                        name: "FK_pedidos_clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedidos_direcciones_cliente_DireccionEntregaIdDireccionClie~",
                        column: x => x.DireccionEntregaIdDireccionCliente,
                        principalTable: "direcciones_cliente",
                        principalColumn: "id_direccion_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedidos_empresas_EmpresaIdEmpresa",
                        column: x => x.EmpresaIdEmpresa,
                        principalTable: "empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_producto = table.Column<string>(type: "text", nullable: false),
                    descripcion_producto = table.Column<string>(type: "text", nullable: false),
                    precio_producto = table.Column<decimal>(type: "numeric", nullable: false),
                    imagen_url = table.Column<string>(type: "text", nullable: false),
                    IdCategoria = table.Column<int>(type: "integer", nullable: false),
                    IdEmpresa = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "integer", nullable: false),
                    EmpresaIdEmpresa = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK_productos_categorias_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "categorias",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_empresas_EmpresaIdEmpresa",
                        column: x => x.EmpresaIdEmpresa,
                        principalTable: "empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "integer", nullable: false),
                    IdProducto = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_pedidos", x => new { x.IdPedido, x.IdProducto });
                    table.ForeignKey(
                        name: "FK_detalle_pedidos_pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "pedidos",
                        principalColumn: "id_pedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_pedidos_productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido_adicionales",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "integer", nullable: false),
                    IdProducto = table.Column<int>(type: "integer", nullable: false),
                    IdAdicional = table.Column<int>(type: "integer", nullable: false),
                    mitad = table.Column<int>(type: "integer", nullable: false),
                    precio_adicional_personalizado = table.Column<decimal>(type: "numeric", nullable: true),
                    DetallePedidoIdPedido = table.Column<int>(type: "integer", nullable: false),
                    DetallePedidoIdProducto = table.Column<int>(type: "integer", nullable: false),
                    AdicionalIdAdicional = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_adicionales", x => new { x.IdPedido, x.IdProducto, x.IdAdicional, x.mitad });
                    table.ForeignKey(
                        name: "FK_pedido_adicionales_adicionales_AdicionalIdAdicional",
                        column: x => x.AdicionalIdAdicional,
                        principalTable: "adicionales",
                        principalColumn: "id_adicionales",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_adicionales_detalle_pedidos_DetallePedidoIdPedido_De~",
                        columns: x => new { x.DetallePedidoIdPedido, x.DetallePedidoIdProducto },
                        principalTable: "detalle_pedidos",
                        principalColumns: new[] { "IdPedido", "IdProducto" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adicionales_EmpresaId",
                table: "adicionales",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_categorias_EmpresaId",
                table: "categorias",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedidos_IdProducto",
                table: "detalle_pedidos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_cliente_ClienteIdCliente",
                table: "direcciones_cliente",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_adicionales_AdicionalIdAdicional",
                table: "pedido_adicionales",
                column: "AdicionalIdAdicional");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_adicionales_DetallePedidoIdPedido_DetallePedidoIdPro~",
                table: "pedido_adicionales",
                columns: new[] { "DetallePedidoIdPedido", "DetallePedidoIdProducto" });

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_ClienteIdCliente",
                table: "pedidos",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_DireccionEntregaIdDireccionCliente",
                table: "pedidos",
                column: "DireccionEntregaIdDireccionCliente");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_EmpresaIdEmpresa",
                table: "pedidos",
                column: "EmpresaIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_productos_CategoriaIdCategoria",
                table: "productos",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_productos_EmpresaIdEmpresa",
                table: "productos",
                column: "EmpresaIdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido_adicionales");

            migrationBuilder.DropTable(
                name: "adicionales");

            migrationBuilder.DropTable(
                name: "detalle_pedidos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "direcciones_cliente");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}
