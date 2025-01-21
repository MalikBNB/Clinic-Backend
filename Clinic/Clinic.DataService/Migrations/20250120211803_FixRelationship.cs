using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.DataService.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MedicalRecords_MedicalRecordId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Payments_PaymentId",
                table: "Appointments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MedicalRecords_AppointmentId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MedicalRecordId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PaymentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalRecordId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Payments_Id",
                table: "Appointments",
                column: "Id",
                principalTable: "Payments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Appointments_AppointmentId",
                table: "MedicalRecords",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Payments_Id",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Appointments_AppointmentId",
                table: "MedicalRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalRecordId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MedicalRecords_AppointmentId",
                table: "MedicalRecords",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalRecordId",
                table: "Appointments",
                column: "MedicalRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PaymentId",
                table: "Appointments",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MedicalRecords_MedicalRecordId",
                table: "Appointments",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Payments_PaymentId",
                table: "Appointments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
