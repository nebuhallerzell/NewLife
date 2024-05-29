using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewLife.Migrations
{
    /// <inheritdoc />
    public partial class AddRentPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rent_Price",
                table: "Rent",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rent_Price",
                table: "Rent");
        }
    }
}
