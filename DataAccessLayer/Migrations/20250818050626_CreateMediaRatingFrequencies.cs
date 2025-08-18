using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class CreateMediaRatingFrequencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anime_AnimeRatingFrequencies_AnimeRatingFrequenciesId",
                table: "Anime");

            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_MangasRatingFrequencies_RatingFrequenciesId",
                table: "Mangas");

            migrationBuilder.DropTable(
                name: "AnimeRatingFrequencies");

            migrationBuilder.DropTable(
                name: "MangasRatingFrequencies");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_RatingFrequenciesId",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Anime_AnimeRatingFrequenciesId",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "RatingFrequenciesId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AnimeRatingFrequenciesId",
                table: "Anime");

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "MediaRatingFrequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaRatingFrequency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaRatingFrequency_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaRatingFrequency_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaRatingFrequency_AnimeId",
                table: "MediaRatingFrequency",
                column: "AnimeId",
                unique: true,
                filter: "[AnimeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MediaRatingFrequency_MangaId",
                table: "MediaRatingFrequency",
                column: "MangaId",
                unique: true,
                filter: "[MangaId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaRatingFrequency");

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingFrequenciesId",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimeRatingFrequenciesId",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimeRatingFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeRatingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangasRatingFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    _1 = table.Column<int>(type: "int", nullable: true),
                    _2 = table.Column<int>(type: "int", nullable: true),
                    _3 = table.Column<int>(type: "int", nullable: true),
                    _4 = table.Column<int>(type: "int", nullable: true),
                    _5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangasRatingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_RatingFrequenciesId",
                table: "Mangas",
                column: "RatingFrequenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeRatingFrequenciesId",
                table: "Anime",
                column: "AnimeRatingFrequenciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anime_AnimeRatingFrequencies_AnimeRatingFrequenciesId",
                table: "Anime",
                column: "AnimeRatingFrequenciesId",
                principalTable: "AnimeRatingFrequencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_MangasRatingFrequencies_RatingFrequenciesId",
                table: "Mangas",
                column: "RatingFrequenciesId",
                principalTable: "MangasRatingFrequencies",
                principalColumn: "Id");
        }
    }
}
