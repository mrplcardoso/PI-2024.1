using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping_api.Migrations
{
    /// <inheritdoc />
    public partial class correcaoclientid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_clients_ClientId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ClientId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "orders",
                newName: "clientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "orders",
                newName: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ClientId",
                table: "orders",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_clients_ClientId",
                table: "orders",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
