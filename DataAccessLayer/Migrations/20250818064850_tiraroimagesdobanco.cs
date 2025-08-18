using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class tiraroimagesdobanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<string>(
                name: "JpgImageUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgLargeImageUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgSmallImageUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpImageUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpLargeImageUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpSmallImageUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgImageUrl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgLargeImageUrl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgSmallImageUrl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpImageUrl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpLargeImageUrl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpSmallImageUrl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JpgImageUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "JpgLargeImageUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "JpgSmallImageUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "WebpImageUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "WebpLargeImageUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "WebpSmallImageUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "JpgImageUrl",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "JpgLargeImageUrl",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "JpgSmallImageUrl",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "WebpImageUrl",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "WebpLargeImageUrl",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "WebpSmallImageUrl",
                table: "Anime");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    JpgImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JpgLargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JpgSmallImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebpImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebpLargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebpSmallImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId",
                unique: true,
                filter: "[AnimeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MangaId",
                table: "Images",
                column: "MangaId",
                unique: true,
                filter: "[MangaId] IS NOT NULL");
        }
    }
}
