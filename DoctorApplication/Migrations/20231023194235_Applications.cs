using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorApplication.Migrations
{
    /// <inheritdoc />
    public partial class Applications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorApplications",
                columns: table => new
                {
                    Appointmentid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Licence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hospitalid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorApplications", x => x.Appointmentid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorApplications");
        }
    }
}
