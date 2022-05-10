using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework.Migrations
{
    public partial class NonWorkingDaysTable_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonWorkingDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonWorkingDays", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonWorkingDays");
        }
    }
}
