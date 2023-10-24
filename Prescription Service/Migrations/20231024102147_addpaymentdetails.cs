using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prescription_Service.Migrations
{
    /// <inheritdoc />
    public partial class addpaymentdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeSessionId",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "StripeSessionId",
                table: "Prescriptions");
        }
    }
}
