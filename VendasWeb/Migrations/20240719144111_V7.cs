using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendasWeb.Migrations
{
    /// <inheritdoc />
    public partial class V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Pedido_PedidoId",
                table: "ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Produto_ProdutoId",
                table: "ItemPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItemPedido_PedidoId",
                table: "ItemPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItemPedido_ProdutoId",
                table: "ItemPedido");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "ItemPedido");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "ItemPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_IdProduto",
                table: "ItemPedido",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Pedido_IdPedido",
                table: "ItemPedido",
                column: "IdPedido",
                principalTable: "Pedido",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Produto_IdProduto",
                table: "ItemPedido",
                column: "IdProduto",
                principalTable: "Produto",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Pedido_IdPedido",
                table: "ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Produto_IdProduto",
                table: "ItemPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItemPedido_IdProduto",
                table: "ItemPedido");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "ItemPedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "ItemPedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_ProdutoId",
                table: "ItemPedido",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Pedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Produto_ProdutoId",
                table: "ItemPedido",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
