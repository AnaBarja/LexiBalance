using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiBalance.Migrations
{
    public partial class cambioPrecio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Precio",
                table: "Productos",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
