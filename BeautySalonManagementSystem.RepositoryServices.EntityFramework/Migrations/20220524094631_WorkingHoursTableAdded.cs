using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework.Migrations
{
    public partial class WorkingHoursTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WorkingHours",
                columns: new[] { "Id", "Time" },
                values: new object[,]
                {
                    { 1, "8:30" },
                    { 2, "9:30" },
                    { 3, "10:30" },
                    { 4, "11:00" },
                    { 5, "12:00" },
                    { 6, "13:30" },
                    { 7, "14:00" },
                    { 8, "15:00" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingHours");
        }
    }
}
