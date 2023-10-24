using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordsManagement.Migrations
{
    /// <inheritdoc />
    public partial class addingappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Records");
        }
    }
}
