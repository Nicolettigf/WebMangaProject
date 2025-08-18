using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class retirarpropsnaodesejadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "ChapterCount",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Serialization",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Subtype",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "VolumeCount",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AnimeCoverImage",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "ageRating",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "ageRatingGuide",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "averageRating",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "endDate",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "episodeCount",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "episodeLength",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "showType",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "startDate",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "subtype",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "youtubeVideoId",
                table: "Anime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AverageRating",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChapterCount",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serialization",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtype",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeCount",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnimeCoverImage",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ageRating",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ageRatingGuide",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "averageRating",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "endDate",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "episodeCount",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "episodeLength",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "showType",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "startDate",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subtype",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "youtubeVideoId",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
