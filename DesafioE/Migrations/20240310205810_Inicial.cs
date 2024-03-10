using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioE.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebCrawlers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTimeExecution = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPages = table.Column<int>(type: "int", nullable: false),
                    NumberOfLines = table.Column<int>(type: "int", nullable: false),
                    ListToSave = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebCrawlers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebCrawlers");
        }
    }
}
