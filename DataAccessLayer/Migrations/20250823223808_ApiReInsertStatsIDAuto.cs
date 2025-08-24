using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class ApiReInsertStatsIDAuto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Remover a PK
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiReInsertStats",
                table: "ApiReInsertStats");

            // 2. Dropar a coluna Id
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApiReInsertStats");

            // 3. Recriar a coluna com Identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApiReInsertStats",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // 4. Recriar a PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiReInsertStats",
                table: "ApiReInsertStats",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApiReInsertStats",
                table: "ApiReInsertStats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApiReInsertStats");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApiReInsertStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApiReInsertStats",
                table: "ApiReInsertStats",
                column: "Id");
        }

    }
}
