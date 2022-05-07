using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework.Migrations
{
    public partial class ScheduledAppointmentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledAppointments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledAppointments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledAppointments_TreatmentId",
                table: "ScheduledAppointments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledAppointments_UserId",
                table: "ScheduledAppointments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledAppointments");
        }
    }
}
