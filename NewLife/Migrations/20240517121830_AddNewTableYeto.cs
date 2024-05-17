using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewLife.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableYeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car_Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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
                    CarImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_Car_Brand_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "Car_Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarBrandId",
                table: "Car",
                column: "CarBrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Car_Brand");
        }
    }
}
