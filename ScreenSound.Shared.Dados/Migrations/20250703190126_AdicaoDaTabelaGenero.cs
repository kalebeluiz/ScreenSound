using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoDaTabelaGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Artistas_artistaId",
                table: "Musicas");

            migrationBuilder.RenameColumn(
                name: "artistaId",
                table: "Musicas",
                newName: "ArtistaId");

            migrationBuilder.RenameIndex(
                name: "IX_Musicas_artistaId",
                table: "Musicas",
                newName: "IX_Musicas_ArtistaId");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Artistas_ArtistaId",
                table: "Musicas",
                column: "ArtistaId",
                principalTable: "Artistas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Artistas_ArtistaId",
                table: "Musicas");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.RenameColumn(
                name: "ArtistaId",
                table: "Musicas",
                newName: "artistaId");

            migrationBuilder.RenameIndex(
                name: "IX_Musicas_ArtistaId",
                table: "Musicas",
                newName: "IX_Musicas_artistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Artistas_artistaId",
                table: "Musicas",
                column: "artistaId",
                principalTable: "Artistas",
                principalColumn: "Id");
        }
    }
}
