using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiVideojuegos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videojuegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videojuegos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EspecVideojuegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideojuegoId = table.Column<int>(type: "int", nullable: false),
                    consola = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecVideojuegos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EspecVideojuegos_Videojuegos_VideojuegoId",
                        column: x => x.VideojuegoId,
                        principalTable: "Videojuegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EspecVideojuegos_VideojuegoId",
                table: "EspecVideojuegos",
                column: "VideojuegoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspecVideojuegos");

            migrationBuilder.DropTable(
                name: "Videojuegos");
        }
    }
}
