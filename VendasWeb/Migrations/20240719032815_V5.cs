using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendasWeb.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPedido",
                table: "ItemPedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPedido",
                table: "ItemPedido",
                columns: new[] { "IdPedido", "IdProduto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPedido",
                table: "ItemPedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPedido",
                table: "ItemPedido",
                column: "IdPedido");
        }
    }
}
