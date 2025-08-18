using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class removeAnimeMangaTitles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anime_AnimeSTitles_AnimeTitlesId",
                table: "Anime");

            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_MangaTitles_TitlesId",
                table: "Mangas");

            migrationBuilder.DropTable(
                name: "AnimeSTitles");

            migrationBuilder.DropTable(
                name: "MangaTitles");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_TitlesId",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Anime_AnimeTitlesId",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "TitlesId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AnimeTitlesId",
                table: "Anime");

            migrationBuilder.RenameColumn(
                name: "synopsis",
                table: "Anime",
                newName: "Synopsis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Synopsis",
                table: "Anime",
                newName: "synopsis");

            migrationBuilder.AddColumn<int>(
                name: "TitlesId",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimeTitlesId",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimeSTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    En_jp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja_jp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    En_jp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ja_jp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_TitlesId",
                table: "Mangas",
                column: "TitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeTitlesId",
                table: "Anime",
                column: "AnimeTitlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anime_AnimeSTitles_AnimeTitlesId",
                table: "Anime",
                column: "AnimeTitlesId",
                principalTable: "AnimeSTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_MangaTitles_TitlesId",
                table: "Mangas",
                column: "TitlesId",
                principalTable: "MangaTitles",
                principalColumn: "Id");
        }
    }
}
