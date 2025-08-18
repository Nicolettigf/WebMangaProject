using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class TodasPropsAntigasRetiradas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanonicalTitle",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "CoverImageLink",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "FavoritesCount",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "PopularityRank",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "PosterImageLink",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "RatingRank",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "UserCount",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AnimePosterImage",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "canonicalTitle",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "favoritesCount",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "popularityRank",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "ratingRank",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "userCount",
                table: "Anime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CanonicalTitle",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageLink",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoritesCount",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PopularityRank",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterImageLink",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RatingRank",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCount",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnimePosterImage",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "canonicalTitle",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "favoritesCount",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "popularityRank",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ratingRank",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userCount",
                table: "Anime",
                type: "int",
                nullable: true);
        }
    }
}
