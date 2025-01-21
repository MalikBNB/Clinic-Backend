using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.DataService.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Payments_Id",
                table: "Appointments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Payments_AppointmentId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Appointments_AppointmentId",
                table: "Payments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Appointments_AppointmentId",
                table: "Payments");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Payments_AppointmentId",
                table: "Payments",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Payments_Id",
                table: "Appointments",
                column: "Id",
                principalTable: "Payments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
