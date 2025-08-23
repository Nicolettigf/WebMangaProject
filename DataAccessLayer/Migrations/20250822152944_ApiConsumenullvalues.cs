using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class ApiConsumenullvalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesConsumed",
                table: "ApiConsumeStats");

            migrationBuilder.AlterColumn<int>(
                name: "UnitarioManga",
                table: "ApiConsumeStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnitarioAnime",
                table: "ApiConsumeStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TotalRequests",
                table: "ApiConsumeStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PagesConsumedAnime",
                table: "ApiConsumeStats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PagesConsumedManga",
                table: "ApiConsumeStats",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesConsumedAnime",
                table: "ApiConsumeStats");

            migrationBuilder.DropColumn(
                name: "PagesConsumedManga",
                table: "ApiConsumeStats");

            migrationBuilder.AlterColumn<int>(
                name: "UnitarioManga",
                table: "ApiConsumeStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitarioAnime",
                table: "ApiConsumeStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalRequests",
                table: "ApiConsumeStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PagesConsumed",
                table: "ApiConsumeStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
