using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class bkashnotificationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringWebhookNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    subscriptionRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentId = table.Column<int>(type: "int", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subscriptionId = table.Column<int>(type: "int", nullable: false),
                    trxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nextPaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trxDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    firstPayment = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringWebhookNotification", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringWebhookNotification");
        }
    }
}
