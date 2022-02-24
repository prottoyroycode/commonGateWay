using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class droballcolbkashnotificationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "dueDate",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "firstPayment",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "nextPaymentDate",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "paymentId",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "paymentStatus",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "subscriptionId",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "subscriptionRequestId",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "timeStamp",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "trxDate",
                table: "RecurringWebhookNotification");

            migrationBuilder.DropColumn(
                name: "trxId",
                table: "RecurringWebhookNotification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "amount",
                table: "RecurringWebhookNotification",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "dueDate",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "firstPayment",
                table: "RecurringWebhookNotification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nextPaymentDate",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "paymentId",
                table: "RecurringWebhookNotification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "paymentStatus",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "subscriptionId",
                table: "RecurringWebhookNotification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "subscriptionRequestId",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "timeStamp",
                table: "RecurringWebhookNotification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "trxDate",
                table: "RecurringWebhookNotification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "trxId",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
