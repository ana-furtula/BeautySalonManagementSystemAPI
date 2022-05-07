using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework.Migrations
{
    public partial class TreatmentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[] { 1, "Neki opis", "Manikir", 1200.0 });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[] { 2, "Neki opis", "Pedikir", 1500.0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Role",
                value: 0);
        }
    }
}
