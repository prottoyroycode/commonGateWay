using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class webhooknotificationtableremap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "RecurringWebhookNotification",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "reverseTrxDate",
                table: "RecurringWebhookNotification",
                newName: "reversTrxDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "trxDate",
                table: "RecurringWebhookNotification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "refundedAmount",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "paymentId",
                table: "RecurringWebhookNotification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                table: "RecurringWebhookNotification",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "reversTrxDate",
                table: "RecurringWebhookNotification",
                newName: "reverseTrxDate");

            migrationBuilder.AlterColumn<string>(
                name: "trxDate",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "refundedAmount",
                table: "RecurringWebhookNotification",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "paymentId",
                table: "RecurringWebhookNotification",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
