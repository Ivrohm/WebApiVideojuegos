using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiVideojuegos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiendaVideojuegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiendaVideojuegos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videojuegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AñodeLanzamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    consola = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videojuegos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reseñas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tiendaVideojuegoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reseñas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reseñas_TiendaVideojuegos_tiendaVideojuegoId",
                        column: x => x.tiendaVideojuegoId,
                        principalTable: "TiendaVideojuegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoTiendaVideojuego",
                columns: table => new
                {
                    VideojuegoId = table.Column<int>(type: "int", nullable: false),
                    tiendaVideojuegoId = table.Column<int>(type: "int", nullable: false),
                    orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoTiendaVideojuego", x => new { x.VideojuegoId, x.tiendaVideojuegoId });
                    table.ForeignKey(
                        name: "FK_VideojuegoTiendaVideojuego_TiendaVideojuegos_tiendaVideojuegoId",
                        column: x => x.tiendaVideojuegoId,
                        principalTable: "TiendaVideojuegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoTiendaVideojuego_Videojuegos_VideojuegoId",
                        column: x => x.VideojuegoId,
                        principalTable: "Videojuegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_tiendaVideojuegoId",
                table: "Reseñas",
                column: "tiendaVideojuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoTiendaVideojuego_tiendaVideojuegoId",
                table: "VideojuegoTiendaVideojuego",
                column: "tiendaVideojuegoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reseñas");

            migrationBuilder.DropTable(
                name: "VideojuegoTiendaVideojuego");

            migrationBuilder.DropTable(
                name: "TiendaVideojuegos");

            migrationBuilder.DropTable(
                name: "Videojuegos");
        }
    }
}
