using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendasWeb.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Cliente_ClienteIdUsuario",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Endereco_EnderecoId",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_EnderecoId",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_ClienteIdUsuario",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "ClienteIdUsuario",
                table: "Endereco");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Bairro",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_CEP",
                table: "Pedido",
                type: "char(9)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Cidade",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Complemento",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Estado",
                table: "Pedido",
                type: "char(2)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Logradouro",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Numero",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Referencia",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Endereco",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                columns: new[] { "IdUsuario", "IdEndereco" });

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Cliente_IdUsuario",
                table: "Endereco",
                column: "IdUsuario",
                principalTable: "Cliente",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Cliente_IdUsuario",
                table: "Endereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Bairro",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_CEP",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Cidade",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Complemento",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Estado",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Logradouro",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Numero",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Referencia",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Endereco");

            migrationBuilder.AddColumn<int>(
                name: "ClienteIdUsuario",
                table: "Endereco",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_EnderecoId",
                table: "Pedido",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ClienteIdUsuario",
                table: "Endereco",
                column: "ClienteIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Cliente_ClienteIdUsuario",
                table: "Endereco",
                column: "ClienteIdUsuario",
                principalTable: "Cliente",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Endereco_EnderecoId",
                table: "Pedido",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
