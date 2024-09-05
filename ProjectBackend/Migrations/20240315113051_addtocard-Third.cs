using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class addtocardThird : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productid",
                table: "AddToCarts",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AddToCarts_ProductId",
                table: "AddToCarts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToCarts_Products_ProductId",
                table: "AddToCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "productid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToCarts_Products_ProductId",
                table: "AddToCarts");

            migrationBuilder.DropIndex(
                name: "IX_AddToCarts_ProductId",
                table: "AddToCarts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "AddToCarts",
                newName: "productid");
        }
    }
}
