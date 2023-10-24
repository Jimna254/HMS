using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription_Service.Migrations
{
    /// <inheritdoc />
    public partial class prescriptiontables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Illness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Patientid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drugs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions");
        }
    }
}
