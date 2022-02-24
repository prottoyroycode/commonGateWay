using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class useridinbaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BkashExecutePaymentWithAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BkashExecutePaymentWithAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BkashCreatePaymentWithAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BkashCreatePaymentWithAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BkashCancelAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BKashAgreementException",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BkashExecutePaymentWithAgreementResponse");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BkashExecutePaymentWithAgreementRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BkashCreatePaymentWithAgreementResponse");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BkashCreatePaymentWithAgreementRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BkashCancelAgreementResponse");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BKashAgreementException");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationUser");
        }
    }
}
