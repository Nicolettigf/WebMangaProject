using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class UpdateAnimeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Theme");
            migrationBuilder.Sql("DELETE FROM Mangas");

            migrationBuilder.DropForeignKey(
                name: "FK_Demographic_Anime_AnimeId",
                table: "Demographic");

            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Relation_RelationId",
                table: "Entry");

            migrationBuilder.DropForeignKey(
                name: "FK_ExplicitGenre_Anime_AnimeId",
                table: "ExplicitGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Anime_AnimeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Jpg_JpgId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Webp_WebpId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Anime_AnimeId",
                table: "Theme");

            migrationBuilder.DropTable(
                name: "Broadcast");

            migrationBuilder.DropTable(
                name: "Jpg");

            migrationBuilder.DropTable(
                name: "Webp");

            migrationBuilder.DropIndex(
                name: "IX_Images_AnimeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_JpgId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_WebpId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "JpgId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "WebpId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "mal_id",
                table: "Theme",
                newName: "MangaId");

            migrationBuilder.RenameColumn(
                name: "mal_id",
                table: "Genre",
                newName: "MangaId");

            migrationBuilder.RenameColumn(
                name: "mal_id",
                table: "ExplicitGenre",
                newName: "MangaId");

            migrationBuilder.RenameColumn(
                name: "RelationId",
                table: "Entry",
                newName: "relationID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Entry",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_RelationId",
                table: "Entry",
                newName: "IX_Entry_relationID");

            migrationBuilder.RenameColumn(
                name: "mal_id",
                table: "Demographic",
                newName: "MangaId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Anime",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "source",
                table: "Anime",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "season",
                table: "Anime",
                newName: "Season");

            migrationBuilder.RenameColumn(
                name: "approved",
                table: "Anime",
                newName: "Approved");

            migrationBuilder.RenameColumn(
                name: "scored_by",
                table: "Anime",
                newName: "ScoredBy");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Theme",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "Theme",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mal_id",
                table: "Streaming",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Streaming",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mal_id",
                table: "Relation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Relation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Relation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "Relation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "JpgImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgLargeImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpgSmallImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MangaId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpLargeImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebpSmallImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mal_id",
                table: "External",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "External",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "ExplicitGenre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "ExplicitGenre",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Demographic",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "Demographic",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Airing",
                table: "Anime",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BroadcastDay",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BroadcastTime",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BroadcastTimezone",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Episodes",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Favorites",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "Anime",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Members",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Anime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Anime",
                type: "float",
                nullable: true);

            

            migrationBuilder.AddColumn<string>(
                name: "Themename",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Themetype",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Themeurl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEnglish",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleJapanese",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Youtube_id",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Youtubeembed_url",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Youtubeurl",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimeThemeSong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeThemeSong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeThemeSong_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Theme_MangaId",
                table: "Theme",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId",
                unique: true,
                filter: "[AnimeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MangaId",
                table: "Images",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MangaId",
                table: "Genre",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExplicitGenre_MangaId",
                table: "ExplicitGenre",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Demographic_MangaId",
                table: "Demographic",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeThemeSong_AnimeId",
                table: "AnimeThemeSong",
                column: "AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demographic_Anime_AnimeId",
                table: "Demographic",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Demographic_Mangas_MangaId",
                table: "Demographic",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Relation_relationID",
                table: "Entry",
                column: "relationID",
                principalTable: "Relation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExplicitGenre_Anime_AnimeId",
                table: "ExplicitGenre",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExplicitGenre_Mangas_MangaId",
                table: "ExplicitGenre",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Mangas_MangaId",
                table: "Genre",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Anime_AnimeId",
                table: "Images",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Mangas_MangaId",
                table: "Images",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Anime_AnimeId",
                table: "Theme",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Mangas_MangaId",
                table: "Theme",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demographic_Anime_AnimeId",
                table: "Demographic");

            migrationBuilder.DropForeignKey(
                name: "FK_Demographic_Mangas_MangaId",
                table: "Demographic");

            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Relation_relationID",
                table: "Entry");

            migrationBuilder.DropForeignKey(
                name: "FK_ExplicitGenre_Anime_AnimeId",
                table: "ExplicitGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ExplicitGenre_Mangas_MangaId",
                table: "ExplicitGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Mangas_MangaId",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Anime_AnimeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Mangas_MangaId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Anime_AnimeId",
                table: "Theme");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Mangas_MangaId",
                table: "Theme");

            migrationBuilder.DropTable(
                name: "AnimeThemeSong");

            migrationBuilder.DropIndex(
                name: "IX_Theme_MangaId",
                table: "Theme");

            migrationBuilder.DropIndex(
                name: "IX_Images_AnimeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_MangaId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Genre_MangaId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_ExplicitGenre_MangaId",
                table: "ExplicitGenre");

            migrationBuilder.DropIndex(
                name: "IX_Demographic_MangaId",
                table: "Demographic");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "mal_id",
                table: "Streaming");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Streaming");

            migrationBuilder.DropColumn(
                name: "mal_id",
                table: "Relation");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Relation");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Relation");

            migrationBuilder.DropColumn(
                name: "url",
                table: "Relation");

            migrationBuilder.DropColumn(
                name: "JpgImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "JpgLargeImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "JpgSmallImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "WebpImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "WebpLargeImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "WebpSmallImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "url",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "mal_id",
                table: "External");

            migrationBuilder.DropColumn(
                name: "type",
                table: "External");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "ExplicitGenre");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "Demographic");

            migrationBuilder.DropColumn(
                name: "Airing",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "BroadcastDay",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "BroadcastTime",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "BroadcastTimezone",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Episodes",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Favorites",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Members",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Anime");

            

            migrationBuilder.DropColumn(
                name: "Themename",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Themetype",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Themeurl",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "TitleEnglish",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "TitleJapanese",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Youtube_id",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Youtubeembed_url",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "Youtubeurl",
                table: "Anime");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "Theme",
                newName: "mal_id");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "Genre",
                newName: "mal_id");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "ExplicitGenre",
                newName: "mal_id");

            migrationBuilder.RenameColumn(
                name: "relationID",
                table: "Entry",
                newName: "RelationId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Entry",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_relationID",
                table: "Entry",
                newName: "IX_Entry_RelationId");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "Demographic",
                newName: "mal_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Anime",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "Anime",
                newName: "source");

            migrationBuilder.RenameColumn(
                name: "Season",
                table: "Anime",
                newName: "season");

            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "Anime",
                newName: "approved");

            migrationBuilder.RenameColumn(
                name: "ScoredBy",
                table: "Anime",
                newName: "scored_by");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Theme",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JpgId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WebpId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "ExplicitGenre",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Demographic",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Broadcast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broadcast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Broadcast_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jpg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmallImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jpg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Webp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmallImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webp", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_JpgId",
                table: "Images",
                column: "JpgId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_WebpId",
                table: "Images",
                column: "WebpId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_AnimeId",
                table: "Broadcast",
                column: "AnimeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Demographic_Anime_AnimeId",
                table: "Demographic",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Relation_RelationId",
                table: "Entry",
                column: "RelationId",
                principalTable: "Relation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExplicitGenre_Anime_AnimeId",
                table: "ExplicitGenre",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Anime_AnimeId",
                table: "Images",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Jpg_JpgId",
                table: "Images",
                column: "JpgId",
                principalTable: "Jpg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Webp_WebpId",
                table: "Images",
                column: "WebpId",
                principalTable: "Webp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Anime_AnimeId",
                table: "Theme",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
