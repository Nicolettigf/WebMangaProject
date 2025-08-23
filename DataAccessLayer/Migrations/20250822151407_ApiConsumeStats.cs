using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class ApiConsumeStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiConsumeStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ApiName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiURLBase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagesConsumed = table.Column<int>(type: "int", nullable: false),
                    UnitarioAnime = table.Column<int>(type: "int", nullable: false),
                    UnitarioManga = table.Column<int>(type: "int", nullable: false),
                    TotalRequests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiConsumeStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiConsumeStats");
        }
    }
}
