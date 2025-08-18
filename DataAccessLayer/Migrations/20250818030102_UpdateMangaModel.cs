using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class UpdateMangaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryManga");

            migrationBuilder.DropIndex(
                name: "IX_Images_MangaId",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Mangas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Chapters",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Favorites",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MalId",
                table: "Mangas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Members",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedFrom",
                table: "Mangas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedTo",
                table: "Mangas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Publishing",
                table: "Mangas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Mangas",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoredBy",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEnglish",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleJapanese",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Volumes",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Serialization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    mal_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serialization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serialization_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_CategoryID",
                table: "Mangas",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MangaId",
                table: "Images",
                column: "MangaId",
                unique: true,
                filter: "[MangaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Author_MangaId",
                table: "Author",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Serialization_MangaId",
                table: "Serialization",
                column: "MangaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Category_CategoryID",
                table: "Mangas",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Category_CategoryID",
                table: "Mangas");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Serialization");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_CategoryID",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Images_MangaId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Chapters",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Favorites",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "MalId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Members",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "PublishedFrom",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "PublishedTo",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Publishing",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "ScoredBy",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "TitleEnglish",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "TitleJapanese",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Volumes",
                table: "Mangas");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Mangas",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryManga",
                columns: table => new
                {
                    GenresID = table.Column<int>(type: "int", nullable: false),
                    MangasIDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryManga", x => new { x.GenresID, x.MangasIDId });
                    table.ForeignKey(
                        name: "FK_CategoryManga_Category_GenresID",
                        column: x => x.GenresID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryManga_Mangas_MangasIDId",
                        column: x => x.MangasIDId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_MangaId",
                table: "Images",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryManga_MangasIDId",
                table: "CategoryManga",
                column: "MangasIDId");
        }
    }
}
