using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class clientuseridcolumninbaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashExecutePaymentWithAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashExecutePaymentWithAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashExecuteAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashExecuteAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashCreatePaymentWithAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashCreatePaymentWithAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashCreateAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashCreateAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BkashCancelAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BKashAgreementException",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "CreateSubscriptionInitiate_Bkash");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashExecutePaymentWithAgreementResponse");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashExecutePaymentWithAgreementRequest");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashExecuteAgreementResponse");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashCreatePaymentWithAgreementResponse");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashCreatePaymentWithAgreementRequest");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashCreateAgreementRequest");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BkashCancelAgreementResponse");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BKashAgreementException");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "ApplicationUser");
        }
    }
}
