using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class addtocardSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToCarts_UsersData_Userid",
                table: "AddToCarts");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "AddToCarts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "cartid",
                table: "AddToCarts",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AddToCarts",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_AddToCarts_Userid",
                table: "AddToCarts",
                newName: "IX_AddToCarts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToCarts_UsersData_UserId",
                table: "AddToCarts",
                column: "UserId",
                principalTable: "UsersData",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToCarts_UsersData_UserId",
                table: "AddToCarts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AddToCarts",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "AddToCarts",
                newName: "cartid");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "AddToCarts",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AddToCarts_UserId",
                table: "AddToCarts",
                newName: "IX_AddToCarts_Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToCarts_UsersData_Userid",
                table: "AddToCarts",
                column: "Userid",
                principalTable: "UsersData",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
