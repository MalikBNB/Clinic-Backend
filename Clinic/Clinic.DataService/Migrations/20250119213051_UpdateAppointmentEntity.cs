using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.DataService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CreatorId",
                table: "Appointments",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ModifierId",
                table: "Appointments",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_CreatorId",
                table: "Appointments",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_ModifierId",
                table: "Appointments",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_CreatorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_ModifierId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CreatorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ModifierId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Appointments");
        }
    }
}
