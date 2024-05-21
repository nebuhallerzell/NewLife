using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewLife.Migrations
{
    /// <inheritdoc />
    public partial class Addnewone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Car_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Car_Km = table.Column<double>(type: "float", nullable: false),
                    Car_Price = table.Column<double>(type: "float", nullable: false),
                    Car_About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Car_Rent = table.Column<bool>(type: "bit", nullable: false),
                    CarImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Adress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
