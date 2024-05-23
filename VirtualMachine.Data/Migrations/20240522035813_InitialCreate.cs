using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualMachine.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Virtual Pet Cat", 9.90m, 8 },
                    { 2, "Virtual Pet Dog", 9.90m, 93 },
                    { 3, "Virtual Garden Kit", 2.90m, 0 },
                    { 4, "Virtual Music Album", 6.90m, 93 },
                    { 5, "Virtual Coffee", 1.90m, 69 },
                    { 6, "Virtual Energy Drink", 3.90m, 27 },
                    { 7, "Virtual Book", 5.90m, 18 },
                    { 8, "Virtual Plant", 2.90m, 76 },
                    { 9, "Virtual Adventure Pass", 3.90m, 6 },
                    { 10, "Virtual Art Piece", 7.90m, 67 },
                    { 11, "Virtual Game Token", 1.90m, 54 },
                    { 12, "Virtual Sunglasses", 1.90m, 64 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
