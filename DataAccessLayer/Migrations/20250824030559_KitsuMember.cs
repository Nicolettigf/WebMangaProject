using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class KitsuMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageLarge",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeLength",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdKitsu",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Nsfw",
                table: "Mangas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterImageLarge",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleSynonyms",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeVideoId",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BroadcastComplete",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageLarge",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeLength",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdKitsu",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Nsfw",
                table: "Anime",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterImageLarge",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleSynonyms",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeImage",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeVideoId",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageLarge",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "EpisodeLength",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "IdKitsu",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Nsfw",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "PosterImageLarge",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "TitleSynonyms",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "YoutubeVideoId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "BroadcastComplete",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "CoverImageLarge",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "EpisodeLength",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "IdKitsu",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Nsfw",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "PosterImageLarge",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "TitleSynonyms",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "YoutubeImage",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "YoutubeVideoId",
                table: "Anime");
        }
    }
}
