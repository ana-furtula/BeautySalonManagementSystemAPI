using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework.Migrations
{
    public partial class AppointmentState_Added_To_ScheduledAppointmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ScheduledAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "ScheduledAppointments");
        }
    }
}
