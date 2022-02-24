using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class CreateSubscriptionInitiateBkashTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateSubscriptionInitiate_Bkash",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subscriptionRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serviceId = table.Column<int>(type: "int", nullable: false),
                    redirectURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subscriptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    firstPaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstPaymentIncludedInCycle = table.Column<bool>(type: "bit", nullable: false),
                    maxCapAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    frequecy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merchantShortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subscriptionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateSubscriptionInitiate_Bkash", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateSubscriptionInitiate_Bkash");
        }
    }
}
