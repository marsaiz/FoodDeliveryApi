using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AddIdEmpresaToAdicionales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "telefono",
                table: "clientes",
                newName: "telefono_cliente");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "clientes",
                newName: "nombre_cliente");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "clientes",
                newName: "email_cliente");

            migrationBuilder.AlterColumn<string>(
                name: "imagen_url",
                table: "productos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "referencia",
                table: "direcciones_cliente",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "telefono_cliente",
                table: "clientes",
                newName: "telefono");

            migrationBuilder.RenameColumn(
                name: "nombre_cliente",
                table: "clientes",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "email_cliente",
                table: "clientes",
                newName: "email");

            migrationBuilder.AlterColumn<string>(
                name: "imagen_url",
                table: "productos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "referencia",
                table: "direcciones_cliente",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
