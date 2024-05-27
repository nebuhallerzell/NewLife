using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewLife.Migrations
{
    /// <inheritdoc />
    public partial class NewTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Car_C_Id",
                table: "Rent");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_User_U_Id",
                table: "Rent");

            migrationBuilder.RenameColumn(
                name: "U_Id",
                table: "Rent",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "C_Id",
                table: "Rent",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_U_Id",
                table: "Rent",
                newName: "IX_Rent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_C_Id",
                table: "Rent",
                newName: "IX_Rent_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Car_CarId",
                table: "Rent",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_User_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Car_CarId",
                table: "Rent");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_User_UserId",
                table: "Rent");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rent",
                newName: "U_Id");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Rent",
                newName: "C_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_UserId",
                table: "Rent",
                newName: "IX_Rent_U_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_CarId",
                table: "Rent",
                newName: "IX_Rent_C_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Car_C_Id",
                table: "Rent",
                column: "C_Id",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_User_U_Id",
                table: "Rent",
                column: "U_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
