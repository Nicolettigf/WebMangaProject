using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class genreitens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimeId1",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MangaId1",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GenreItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreItem_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GenreItem_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_AnimeId1",
                table: "Genre",
                column: "AnimeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MangaId1",
                table: "Genre",
                column: "MangaId1");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItem_AnimeId",
                table: "GenreItem",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItem_MangaId",
                table: "GenreItem",
                column: "MangaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Anime_AnimeId1",
                table: "Genre",
                column: "AnimeId1",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Mangas_MangaId1",
                table: "Genre",
                column: "MangaId1",
                principalTable: "Mangas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Anime_AnimeId1",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Mangas_MangaId1",
                table: "Genre");

            migrationBuilder.DropTable(
                name: "GenreItem");

            migrationBuilder.DropIndex(
                name: "IX_Genre_AnimeId1",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_MangaId1",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "AnimeId1",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "MangaId1",
                table: "Genre");
        }
    }
}
